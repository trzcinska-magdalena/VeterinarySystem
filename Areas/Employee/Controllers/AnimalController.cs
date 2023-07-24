using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using VeterinarySystem.Data;
using VeterinarySystem.Models;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Models.ViewModels;
using VeterinarySystem.Repository.IRepository;

namespace VeterinarySystem.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = UserRole.Role_Admin + "," + UserRole.Role_Employee)]
    public class AnimalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        AnimalDetailViewModel animalDetailViewModel = new AnimalDetailViewModel();
        public AnimalController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<AnimalCreateViewModel> ConstructAnimalCreateVMAsync()
        {
            var breeds = await _unitOfWork.Breeds.GetAllAsync();
            var clients = await _unitOfWork.Clients.GetAllAsync();
            var animalCreateViewModel = new AnimalCreateViewModel
            {
                Breeds = breeds.Select(breed =>
                new SelectListItem
                {
                    Value = breed.Id.ToString(),
                    Text = breed.Name
                }),

                Clients = clients.Select(client =>
                new SelectListItem
                {
                    Value = client.Id.ToString(),
                    Text = $"{client.FirstName} {client.LastMame}"
                }),
            };
            return animalCreateViewModel;
        }
        public async Task<AnimalDetailViewModel> ConstructAnimalDetailVMAsync()
        {
            var typeOfVaccines = await _unitOfWork.TypeOfVaccines.GetAllAsync();
            var vaccinations = await _unitOfWork.Vaccinations.GetAllAsync();
            var medicines = await _unitOfWork.Medicines.GetAllAsync();
            var surgeries = await _unitOfWork.Surgeries.GetAllAsync();
            var vets = await _unitOfWork.Vets.GetAllAsync();

            animalDetailViewModel.TypeOfVaccines = typeOfVaccines.Select(a =>
            new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            });

            animalDetailViewModel.Vaccinations = vaccinations.GroupBy(a => a.TypeOfVaccine.Name).ToDictionary(e => e.Key, e => e.ToList());

            animalDetailViewModel.Medicines = medicines.Select(a =>
            new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            });
            animalDetailViewModel.Surgeries = surgeries.Select(a =>
            new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            });
            animalDetailViewModel.Vets = vets.Select(a =>
                new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = $"{a.FirstName} {a.LastName}"
                });


            return animalDetailViewModel;
        }

        public async Task<IActionResult> Index()
        {
            var animalViewModel = new AnimalViewModel
            {
                Animals = await _unitOfWork.Animals.GetAllAsync(x => x.Breed, x => x.Client)
            };
            return View(animalViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            var animalViewModel = new AnimalViewModel
            {
                Animals = await _unitOfWork.Animals.GetAllAsync(x => x.Breed, x => x.Client)
            };

            if (!string.IsNullOrEmpty(searchString))
            {
                animalViewModel.Animals = animalViewModel.Animals.Where(e => e.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }
            return View(animalViewModel);
        }

        public IActionResult Create()
        {
            return View(ConstructAnimalCreateVMAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Animal animal)
        {
            var client = await _unitOfWork.Clients.GetAsync(e => e.Id == animal.ClientId);
            var breed = await _unitOfWork.Breeds.GetAsync(e => e.Id == animal.BreedId);

            if (client == null || breed == null || !ModelState.IsValid)
            {
                return View(ConstructAnimalCreateVMAsync());
            }

            animal.Client = client;
            animal.Breed = breed;

            _unitOfWork.Animals.Add(animal);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Animal created successfully";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _unitOfWork.Animals.GetAsync(e => e.Id == id, x => x.Breed, x => x.Client, x => x.Weights);
            if (animal == null)
            {
                return NotFound();
            }

            var animalDetailModel = ConstructAnimalDetailVMAsync();
            animalDetailModel.Result.Animal = animal;
            animalDetailModel.Result.Appointments = await _unitOfWork.Appointments.GetAppointmentsWithAllData((int)id);

            return View(animalDetailModel.Result);
        }

        public async Task<IActionResult> AddNewWeightAsync(int? id, Weight newWeight)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _unitOfWork.Animals.GetAsync(e => e.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            newWeight.Animal = animal;

            if (newWeight.Date < DateTime.Now)
            {
                TempData["warning"] = "The entered date is earlier than today's date";

            }
            else if (newWeight.Date > DateTime.Now)
            {
                TempData["warning"] = "The entered date is later than today's date";
            }
            if (newWeight.Value > 0 && newWeight.Date != new DateTime(0001, 01, 01, 00, 00, 00))
            {
                _unitOfWork.Weights.Add(newWeight);
                await _unitOfWork.SaveAsync();
                TempData["success"] = "Weight added successfully";
            }
            return RedirectToAction("Detail", new { id });
        }

        public async Task<IActionResult> AddNewVaccinationAsync(int? id, Vaccination newVaccination)
        {
            if (id == null)
            {
                return NotFound();
            }
            newVaccination.Animal = await _unitOfWork.Animals.GetAsync(e => e.Id == id);

            if (newVaccination.TypeOfVaccineId != 0 && newVaccination.Date != new DateTime(0001, 01, 01, 00, 00, 00) && newVaccination.ExpiryDate != new DateTime(0001, 01, 01, 00, 00, 00))
            {
                newVaccination.TypeOfVaccine = await _unitOfWork.TypeOfVaccines.GetAsync(e => e.Id == newVaccination.TypeOfVaccine.Id);

                _unitOfWork.Vaccinations.Add(newVaccination);
                await _unitOfWork.SaveAsync();
                TempData["success"] = "Vaccination added successfully";
            }
            return RedirectToAction("Detail", new { id });
        }

        public async Task<IActionResult> AddNewAppointmentAsync(int? id, Appointment newAppointment)
        {
            if (id == null)
            {
                return NotFound();
            }
            newAppointment.Animal = await _unitOfWork.Animals.GetAsync(e => e.Id == id);

            if (newAppointment.Date != new DateTime(0001, 01, 01, 00, 00, 00) && string.IsNullOrEmpty(newAppointment.Description))
            {
                _unitOfWork.Appointments.Add(newAppointment);
                await _unitOfWork.SaveAsync();
                TempData["success"] = "Appointment added successfully";
            }
            return RedirectToAction("Detail", new { id });
        }

        public async Task<IActionResult> UpdateWeightsAsync(string label, int value, int id)
        {
            var weight = await _unitOfWork.Weights.GetAsync(e => e.AnimalId == id && e.Date == DateTime.Parse(label));

            if (value == 0)
            {
                _unitOfWork.Weights.Remove(weight);
                await _unitOfWork.SaveAsync();
                TempData["success"] = "Weight deleted successfully";
            }
            else
            {
                weight.Value = value;
                _unitOfWork.Weights.Update(weight);
                await _unitOfWork.SaveAsync();
                TempData["success"] = "Weight updated successfully";
            }
            return Ok();
        }

        public async Task<IActionResult> GetAllAppointmentsAsync(int id)
        {
            var appointmants = await _unitOfWork.Appointments.GetAppointmentsWithAllData(id);

            var events = appointmants.Select(e => new Event
            {
                Id = e.Id,
                Title = e.Description,
                Description = e.Description,
                Start = e.Date.ToString("yyyy-MM-dd HH:mm")
            });

            return new JsonResult(events);
        }

        public async Task<IActionResult> ShowAppointmentInfoAsync(int id)
        {
            Console.WriteLine(id);
            animalDetailViewModel.Appointment = await _unitOfWork.Appointments.GetAsync(x => x.Id == id);
            return Ok();
        }
    }
}
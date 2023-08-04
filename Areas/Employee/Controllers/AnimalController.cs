using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Linq.Expressions;
using VeterinarySystem.Data;
using VeterinarySystem.Models;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Models.ViewModels;
using VeterinarySystem.Repository.IRepository;
using VeterinarySystem.Service.IService;

namespace VeterinarySystem.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = UserRole.Role_Admin + "," + UserRole.Role_Employee)]
    public class AnimalController : Controller
    {
        private readonly ISystemService _systemService;
        private readonly IUnitOfWork _unitOfWork;
        public AnimalController(ISystemService systemService, IUnitOfWork unitOfWork)
        {
            _systemService = systemService;
            _unitOfWork = unitOfWork;
        }

        public void SetTempData(string tempType, string tempDataValue)
        {
            TempData[tempType] = tempDataValue;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var animalViewModel = await _systemService.ConstructAnimalsVWAsync();
                return View(animalViewModel);
            }
            catch (Exception ex)
            {
                //_systemService.SetLogError(ex);
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            try
            {
                var animalViewModel = await _systemService.ConstructAnimalsVWAsync(searchString);
                return View(animalViewModel);
            }
            catch (Exception ex)
            {
                //_systemService.SetLogError(ex);
                return RedirectToAction("Index", "Error");
            }
        }

        public async Task<IActionResult> Create()
        {
            return View(await _systemService.ConstructAnimalCreateVMAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Animal animal)
        {
            try
            {
                if(ModelState.IsValid && await _systemService.AddNewAnimalAsync(animal))
                {
                    SetTempData("success", "The animal was created successfully");
                    return RedirectToAction("Index");
                }
                return View(await _systemService.ConstructAnimalCreateVMAsync());
            }
            catch (Exception ex)
            {
                //_systemService.SetLogError(ex);
                return RedirectToAction("Index", "Error");
            }
        }

        public async Task<IActionResult> Detail(int? id, string activeTab)
        {
            var animal = await _systemService.GetAnimalWithDetails(id);
            
            if(animal != null)
            {
                var animalDetailModel = await _systemService.ConstructAnimalDetailVMAsync(animal, activeTab);
                return View(animalDetailModel);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddNewWeightAsync(int? id, Weight newWeight)
        {
            if (!_systemService.IsValidateWeightWithDate(newWeight))
            {
                SetTempData("warning", "Invalid weight value or date.");
            }
            else
            {
                var animal = await _systemService.GetAnimalWithDetails(id);
                if (animal != null && await _systemService.SetWeightToAnimal(newWeight, animal))
                {
                    SetTempData("success", "The weight was added successfully.");
                }
            }
            return RedirectToAction("Detail", new { id, activeTab = "Weight" });
        }


        // TODO
        public async Task<IActionResult> UpdateWeightsAsync(string label, int value, int id)
        {
            var weight = await _systemService.GetWeight(id, DateTime.Parse(label));

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

        public async Task<IActionResult> AddNewVaccinationAsync(int? id, Vaccination newVaccination)
        {
            if (id == null)
            {
                return NotFound();
            }
         
            if(!_systemService.IsValidateVaccination(newVaccination))
            {
                //_systemService.SetTempData(TempData, "warning", "Invalid type of vaccine or date.");
            }
            else
            {
                newVaccination.AnimalId = (int)id;
                newVaccination.TypeOfVaccineId = newVaccination.TypeOfVaccineId;

                _unitOfWork.Vaccinations.Add(newVaccination);
                await _unitOfWork.SaveAsync();
                //_systemService.SetTempData(TempData, "success", "The vaccination was added successfully.");
            }
            return RedirectToAction("Detail", new { id, activeTab = "Vaccination" });
        }

        public async Task<IActionResult> AddNewAppointmentAsync(int? id, Appointment newAppointment)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (newAppointment.Date == default || string.IsNullOrEmpty(newAppointment.Description))
            {
                TempData["warning"] = "Invalid description or date";
            }
            else
            {
                newAppointment.Animal = await _unitOfWork.Animals.GetAsync(filter: e => e.Id == id, tracking: false);
                _unitOfWork.Appointments.Add(newAppointment);
                await _unitOfWork.SaveAsync();
                TempData["success"] = "Appointment added successfully";
            }
            return RedirectToAction("Detail", new { id, activeTab = "Appointment" });
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

            //animalDetailViewModel.Appointment = await _unitOfWork.Appointments.GetAsync(filter: e => e.Id == id, tracking: false);
            return Ok();
        }
    }
}
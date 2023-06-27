﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Models.ViewModels;
using VeterinarySystem.Repository.IRepository;

namespace VeterinarySystem.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class AnimalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnimalController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public AnimalCreateViewModel ConstructAnimalCreateVM()
        {

            var animalCreateViewModel = new AnimalCreateViewModel
            {
                Breeds = _unitOfWork.Breeds.GetAll().Select(breed =>
                new SelectListItem
                {
                    Value = breed.Id.ToString(),
                    Text = breed.Name
                }),

                Clients = _unitOfWork.Clients.GetAll().Select(client =>
                new SelectListItem
                {
                    Value = client.Id.ToString(),
                    Text = $"{client.FirstName} {client.LastMame}"
                }),
            };
            return animalCreateViewModel;
        }
        public AnimalDetailViewModel ConstructAnimalDetailVM()
        {
            var animalDetailViewModel = new AnimalDetailViewModel
            {
                TypeOfVaccines = _unitOfWork.TypeOfVaccines.GetAll().Select(a =>
                new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                }),

                Vaccinations = _unitOfWork.Vaccinations.GetAll().GroupBy(a => a.TypeOfVaccine.Name).ToDictionary(e => e.Key, e => e.ToList()),
            };
            return animalDetailViewModel;
        }

        public IActionResult Index()
        {
            var animalViewModel = new AnimalViewModel
            {
                Animals = _unitOfWork.Animals.GetAllWithData(null).ToList()
            };
            return View(animalViewModel);
        }

        [HttpGet]
        public IActionResult Index(string searchString)
        {
            var animalViewModel = new AnimalViewModel
            {
                Animals = _unitOfWork.Animals.GetAllWithData(searchString).ToList()
            };
            return View(animalViewModel);
        }

        public IActionResult Create()
        {
            return View(ConstructAnimalCreateVM());
        }

        [HttpPost]
        public IActionResult Create(Animal animal)
        {
            var client = _unitOfWork.Clients.Get(e => e.Id == animal.ClientId);
            var breed = _unitOfWork.Breeds.Get(e => e.Id == animal.BreedId);

            if (client == null || breed == null || !ModelState.IsValid)
            {
                return View(ConstructAnimalCreateVM());
            }

            animal.Client = client;
            animal.Breed = breed;

            _unitOfWork.Animals.Add(animal);
            _unitOfWork.Save();
            TempData["success"] = "Animal created successfully";

            return RedirectToAction("Index");
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = _unitOfWork.Animals.GetWithAllData((int)id);
            if (animal == null)
            {
                return NotFound();
            }

            var animalDetailModel = ConstructAnimalDetailVM();
            animalDetailModel.Animal = animal;
            animalDetailModel.Appointments = _unitOfWork.Appointment.GetAppointmentsWithAllData((int)id).ToList();

            return View(animalDetailModel);
        }

        public IActionResult AddNewWeight(int? id, Weight newWeight)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = _unitOfWork.Animals.Get(e => e.Id == id);
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
                _unitOfWork.Save();
                TempData["success"] = "Weight added successfully";
            }
            return RedirectToAction("Detail", new { id });
        }

        public IActionResult AddNewVaccination(int? id, Vaccination newVaccination)
        {
            if (id == null)
            {
                return NotFound();
            }
            newVaccination.Animal = _unitOfWork.Animals.Get(e => e.Id == id);

            if (newVaccination.TypeOfVaccineId != 0 && newVaccination.Date != new DateTime(0001, 01, 01, 00, 00, 00) && newVaccination.ExpiryDate != new DateTime(0001, 01, 01, 00, 00, 00))
            {
                newVaccination.TypeOfVaccine = _unitOfWork.TypeOfVaccines.Get(e => e.Id == newVaccination.TypeOfVaccine.Id);

                _unitOfWork.Vaccinations.Add(newVaccination);
                _unitOfWork.Save();
                TempData["success"] = "Vaccination added successfully";
            }
            return RedirectToAction("Detail", new { id });
        }
    }
}
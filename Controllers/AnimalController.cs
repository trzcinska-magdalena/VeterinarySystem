using DogVetSystem_Razor.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using VeterinarySystem.Models;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Repository;
using VeterinarySystem.Repository.IRepository;

namespace VeterinarySystem.Controllers
{
    public class AnimalController : Controller
    {    
        private readonly IUnitOfWork _unitOfWork;

        public AnimalViewModel AnimalViewModel { get; set; }

        public CreateViewModel CreateViewModel { get; set; }

        public AnimalController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            AnimalViewModel = new AnimalViewModel();
        }

        public IActionResult Index()
        {
            AnimalViewModel.Animals = _unitOfWork.Animals.GetAllWithData(null).ToList();
            return View(AnimalViewModel);
        }

        [HttpGet]
        public IActionResult Index(string searchString)
        {
            AnimalViewModel.Animals = _unitOfWork.Animals.GetAllWithData(searchString).ToList();
            return View(AnimalViewModel);
        }

        public CreateViewModel PowerInputs_Create()
        {
            CreateViewModel = new CreateViewModel();

            CreateViewModel.Breeds = _unitOfWork.Breeds.GetAll().ToList().ConvertAll(breed =>
            {
                return new SelectListItem()
                {
                    Value = breed.Id.ToString(),
                    Text = breed.Name
                };
            });

            CreateViewModel.Clients = _unitOfWork.Clients.GetAll().ToList().ConvertAll(client =>
            {
                return new SelectListItem()
                {
                    Value = client.Id.ToString(),
                    Text = client.FirstName + " " + client.LastMame
                };
            });
            return CreateViewModel;
        }

        public IActionResult Create()
        {
            return View(PowerInputs_Create());
        }
        [HttpPost]
        public IActionResult Create(AnimalPOST animal)
        {           
            Client? client = _unitOfWork.Clients.Get(e => e.Id == animal.ClientId);
            Breed? breed = _unitOfWork.Breeds.Get(e => e.Id == animal.BreedId);

            if (client == null || breed == null || !ModelState.IsValid)
            {
                return View(PowerInputs_Create());
            }

            _unitOfWork.Animals.Add(new Animal()
            {
                Name = animal.Name,
                BirthDate = animal.BirthDate,
                Gender = animal.Gender,
                Breed = breed,
                Client = client
            });

            _unitOfWork.Save();
            TempData["success"] = "Animal created successfully";

            return RedirectToAction("index");
        }

        public AnimalDetailModel SetBasicData_Detail(int animalId)
        {
            AnimalDetailModel animalDetailModel = new AnimalDetailModel();

            animalDetailModel.Animal = _unitOfWork.Animals.GetWithAllData((int)animalId);
            animalDetailModel.Weights = _unitOfWork.Weights.GetAll().Where(e => e.AnimalId == animalId).ToList();

            animalDetailModel.TypeOfVaccines = _unitOfWork.TypeOfVaccines.GetAll().ToList().ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                };
            });

            animalDetailModel.Vaccinations = _unitOfWork.Vaccinations.GetAll().GroupBy(e => e.TypeOfVaccine.Name).ToDictionary(e => e.Key, e => e.ToList());

            //animalDetailModel.Appointments = _unionOfWork.Appointments.GetAppointmentsWithAllData((int)animalId).ToList();
            return animalDetailModel;
        }

        public IActionResult Detail(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            
            return View(SetBasicData_Detail((int)id));
        }
    }
}

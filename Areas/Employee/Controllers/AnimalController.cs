using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
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

        public async Task<IActionResult> Detail([Required]int id, string activeTab)
        {
            if(!_systemService.IsValidTheActiveTab(activeTab))
            {
                return RedirectToAction("Index");
            }

            try
            {
                var animal = await _systemService.GetAnimalWithDetails(id);
                if (animal != null)
                {
                    var animalDetailModel = await _systemService.ConstructAnimalDetailVMAsync(animal, activeTab);
                    return View(animalDetailModel);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //_systemService.SetLogError(ex);
                return RedirectToAction("Index", "Error");
            }        
        }

        public async Task<IActionResult> AddNewWeightAsync([Required]int id, Weight newWeight)
        {
            if (!TryValidateModel(newWeight))
            {
                SetTempData("warning", "Invalid weight value or date.");
            }
            else
            {
                try
                {
                    var animal = await _systemService.GetAnimalWithDetails(id);
                    if (animal != null && await _systemService.AddWeightToAnimal(newWeight, animal))
                    {
                        SetTempData("success", "The weight was added successfully.");
                    }
                }
                catch (Exception ex)
                {
                    //_systemService.SetLogError(ex);
                    return RedirectToAction("Index", "Error");
                }              
            }

            return RedirectToAction("Detail", new { id, activeTab = "Weight" });
        }

        public async Task<IActionResult> UpdateWeightsAsync(string label, int value, int id)
        {
            try
            {
                var weight = await _systemService.GetWeight(id, DateTime.Parse(label));

                (var success, var message) = await _systemService.UpdateWeight(weight, value);

                if (!success)
                {
                    return Conflict();
                }
                TempData["success"] = message;
                return Ok();
            }
            catch (Exception ex)
            {
                //_systemService.SetLogError(ex);
                return RedirectToAction("Index", "Error");
            }
        }

        public async Task<IActionResult> AddNewVaccinationAsync([Required] int id, Vaccination newVaccination)
        {
            if(!TryValidateModel(newVaccination))
            {
                SetTempData("warning", "Invalid type of vaccine or date.");
            }
            else
            {
                try
                {
                    if (await _systemService.AddVaccinationToAnimal(newVaccination, id))
                    {
                        SetTempData("success", "The vaccination was added successfully.");
                    }
                }
                catch (Exception ex)
                {
                    //_systemService.SetLogError(ex);
                    return RedirectToAction("Index", "Error");
                }
            }
            return RedirectToAction("Detail", new { id, activeTab = "Vaccination" });
        }

        public async Task<IActionResult> GetAllAppointmentsAsync(int id)
        {
            try
            {
                return new JsonResult(await _systemService.GetAllAppointmentsAsEvent(id));
            }
            catch (Exception ex)
            {
                //_systemService.SetLogError(ex);
                return Conflict();
            }          
        }

        public async Task<IActionResult> ShowAppointmentInfoAsync(int id)
        {
            Console.WriteLine(id);

            //animalDetailViewModel.Appointment = await _unitOfWork.Appointments.GetAsync(filter: e => e.Id == id, tracking: false);
            return Ok();
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Principal;
using VeterinarySystem.Data;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Models.ViewModels;
using VeterinarySystem.Repository.IRepository;

namespace VeterinarySystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRole.Role_Admin)]
    public class BaseManagementController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public BaseManagementController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public BaseManagementViewModel ConstructBaseManagementVM()
        {
            var baseManagementViewModel = new BaseManagementViewModel
            {
                Breeds = _unitOfWork.Breeds.GetAll()
            };
            return baseManagementViewModel;
        }
        public IActionResult Index(string type)
        {
            return View(ConstructBaseManagementVM());
        }

        [HttpPost]
        public IActionResult AddNewBreed(Breed newBreed)
        {
            if (_unitOfWork.Breeds.GetAll().Any(e => e.Name == newBreed.Name))
            {
                TempData["error"] = "This breed exist!";
                return View("Index", ConstructBaseManagementVM());
            }
            if (!ModelState.IsValid)
            {
                return View("Index", ConstructBaseManagementVM());
            }
            
            _unitOfWork.Breeds.Add(newBreed);
            _unitOfWork.Save();
            TempData["success"] = "The breed created successfully";
            return RedirectToAction("Index", new { type = "Breed" });
        }

        [HttpPost]
        public IActionResult UpdateBreed(Breed newBreed)
        {
            var existingBreed= _unitOfWork.Breeds.GetAll().Where(e => e.Name == newBreed.Name).FirstOrDefault();
            if (existingBreed != null && existingBreed.Id != newBreed.Id)
            {
                TempData["error"] = "This breed exist!";
                return View("Index", ConstructBaseManagementVM());
            }
            if (!ModelState.IsValid)
            {
                return View("Index", ConstructBaseManagementVM());
            }

            var breed = _unitOfWork.Breeds.Get(e => e.Id == newBreed.Id);
            if (breed == null)
            {
                return View("Index", ConstructBaseManagementVM());
            }

            breed.Name = newBreed.Name;

            _unitOfWork.Breeds.Update(breed);
            _unitOfWork.Save();
            TempData["success"] = "The bread updated successfully";
            return RedirectToAction("Index", new { type = "Breed" });
        }


        [HttpPost, ActionName("DeleteBreed")]
        public IActionResult DeleteBreed(int id)
        {
            var breed = _unitOfWork.Breeds.Get(e => e.Id == id);
            if (breed == null)
            {
                return View("Index", ConstructBaseManagementVM());
            }

            _unitOfWork.Breeds.Remove(breed);
            _unitOfWork.Save();
            TempData["success"] = "The breed removed successfully";
            return RedirectToAction("Index", new { type = "Breed" });
        }


    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<BaseManagementViewModel> ConstructBaseManagementVMAsync()
        {
            var baseManagementViewModel = new BaseManagementViewModel
            {
                Breeds = await _unitOfWork.Breeds.GetAllAsync()
            };
            return baseManagementViewModel;
        }
        public IActionResult Index(string type)
        {
            return View(ConstructBaseManagementVMAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBreedAsync(Breed newBreed)
        {
            var breeds = await _unitOfWork.Breeds.GetAllAsync();
            if (breeds.Any(e => e.Name == newBreed.Name))
            {
                TempData["error"] = "This breed exist!";
                return View("Index", ConstructBaseManagementVMAsync());
            }
            if (!ModelState.IsValid)
            {
                return View("Index", ConstructBaseManagementVMAsync());
            }
            
            _unitOfWork.Breeds.Add(newBreed);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "The breed created successfully";
            return RedirectToAction("Index", new { type = "Breed" });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBreedAsync(Breed newBreed)
        {
            var breeds = await _unitOfWork.Breeds.GetAllAsync();
            var existingBreed = breeds.Where(e => e.Name == newBreed.Name).FirstOrDefault();
            if (existingBreed != null && existingBreed.Id != newBreed.Id)
            {
                TempData["error"] = "This breed exist!";
                return View("Index", ConstructBaseManagementVMAsync());
            }
            if (!ModelState.IsValid)
            {
                return View("Index", ConstructBaseManagementVMAsync());
            }

            var breed = await _unitOfWork.Breeds.GetAsync(e => e.Id == newBreed.Id);
            if (breed == null)
            {
                return View("Index", ConstructBaseManagementVMAsync());
            }

            breed.Name = newBreed.Name;

            _unitOfWork.Breeds.Update(breed);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "The bread updated successfully";
            return RedirectToAction("Index", new { type = "Breed" });
        }


        [HttpPost, ActionName("DeleteBreed")]
        public async Task<IActionResult> DeleteBreedAsync(int id)
        {
            var breed = await _unitOfWork.Breeds.GetAsync(e => e.Id == id);
            if (breed == null)
            {
                return View("Index", ConstructBaseManagementVMAsync());
            }

            _unitOfWork.Breeds.Remove(breed);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "The breed removed successfully";
            return RedirectToAction("Index", new { type = "Breed" });
        }
    }
}

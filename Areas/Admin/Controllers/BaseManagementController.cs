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
        public async Task<IActionResult> Index()
        {
            var model = await ConstructBaseManagementVMAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBreedAsync(Breed newBreed)
        {
            var breeds = await _unitOfWork.Breeds.GetAllAsync();
            var model = await ConstructBaseManagementVMAsync();

            if (breeds.Any(e => e.Name == newBreed.Name))
            {
                TempData["error"] = "This breed exist!";
                return View("Index", model);
            }
            else if (!ModelState.IsValid)
            {
                return View("Index", model);
            }
            
            _unitOfWork.Breeds.Add(newBreed);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "The breed was created successfully";
            return RedirectToAction("Index", new { type = "Breed" });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBreedAsync(Breed updatedBreed)
        {
            Console.Write(updatedBreed.Id + " name " + updatedBreed.Name);

            var existingBreed = await _unitOfWork.Breeds.GetAsync(e => e.Name == updatedBreed.Name);


            var model = await ConstructBaseManagementVMAsync();

            if (existingBreed != null && existingBreed.Id != updatedBreed.Id)
            {
                TempData["error"] = "This breed exist!";
                return View("Index", model);
            }
            else if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var breedToUpdate = await _unitOfWork.Breeds.GetAsync(e => e.Id == updatedBreed.Id);
            if (breedToUpdate == null)
            {
                return View("Index", model);
            }
            breedToUpdate.Name = updatedBreed.Name;

            _unitOfWork.Breeds.Update(breedToUpdate);
            await _unitOfWork.SaveAsync();

            TempData["success"] = "The bread was updated successfully";
            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("DeleteBreed")]
        public async Task<IActionResult> DeleteBreedAsync(int id)
        {
            var breed = await _unitOfWork.Breeds.GetAsync(e => e.Id == id);
            if (breed == null)
            {
                TempData["error"] = "The breed was not found";
            }
            else
            {
                _unitOfWork.Breeds.Remove(breed);
                await _unitOfWork.SaveAsync();
                TempData["success"] = "The breed was removed successfully";
            }
            return RedirectToAction("Index");
        }
    }
}

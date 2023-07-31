using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq.Expressions;
using VeterinarySystem.Data;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Models.ViewModels;
using VeterinarySystem.Repository;
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
                AllBreeds = await _unitOfWork.Breeds.GetAllAsync(tracking: false)
            };
            return baseManagementViewModel;
        }
        public async Task<IActionResult> Index()
        {
            var model = await ConstructBaseManagementVMAsync();
            return View(model);
        }

        private void SetTempData(string tempType, string tempData)
        {
            TempData[tempType] = tempData;
        }

        private async Task SaveBreedInTheBaseAsync(Breed breed)
        {
            try
            {
                _unitOfWork.Breeds.Add(breed);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                // Log the error details to the log file

                throw new Exception(ex.Message);
            }

        }

        private async Task UpdateBreedInTheBaseAsync(Breed breed)
        {
            try
            {
                _unitOfWork.Breeds.Update(breed);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                // Log the error details to the log file

                throw new Exception(ex.Message);
            }
        }

        private async Task RemoveBreedInTheBaseAsync(Breed breed)
        {
            try
            {
                _unitOfWork.Breeds.Remove(breed);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                // TODO - Log the error details to the log file

                throw new Exception(ex.Message);
                
            }
        }

        private async Task<Breed?> GetBreedOrNullAsync(Expression<Func<Breed, bool>> filter)
        {
            try
            {
                return await _unitOfWork.Breeds.GetAsync(filter);
            }
            catch (Exception ex)
            {
                // TODO - Log the error details to the log file

                throw new Exception(ex.Message);            
            }
        }

        [HttpPost, ActionName("AddNewBreed")]
        public async Task<IActionResult> AddNewBreedAsync(Breed newBreed)
        {
            if (await GetBreedOrNullAsync(e => e.Name == newBreed.Name) != null)
            {
                SetTempData("error", "This breed exists!");
            }
            else if (ModelState.IsValid)
            {
                await SaveBreedInTheBaseAsync(newBreed);
                SetTempData("success", "The breed was created successfully");

                return RedirectToAction("Index");
            }

            var model = await ConstructBaseManagementVMAsync();
            return View("Index", model);
        }

        [HttpPost, ActionName("UpdateBreed")]
        public async Task<IActionResult> UpdateBreedAsync(Breed newBreed)
        {
            if (await GetBreedOrNullAsync(e => e.Name == newBreed.Name && e.Id != newBreed.Id) != null)
            {
                SetTempData("error", "This breed exists!");
            }
            else if (ModelState.IsValid)
            {
                var breedToUpdate = await GetBreedOrNullAsync(e => e.Id == newBreed.Id && e.Name != newBreed.Name);
                if (breedToUpdate != null)
                {
                    breedToUpdate.Name = newBreed.Name;

                    await UpdateBreedInTheBaseAsync(breedToUpdate);
                    SetTempData("success", "The breed was updated successfully");

                    return RedirectToAction("Index");
                }
            }
            var model = await ConstructBaseManagementVMAsync();
            return View("Index", model);
        }


        [HttpPost, ActionName("DeleteBreed")]
        public async Task<IActionResult> DeleteBreedAsync(int id)
        {
            var breed = await GetBreedOrNullAsync(e => e.Id == id);
            if (breed == null)
            {
                SetTempData("error", "The breed was not found!");
            }
            else
            {
                await RemoveBreedInTheBaseAsync(breed);
                SetTempData("success", "The breed was removed successfully");

                return RedirectToAction("Index");
            }
            var model = await ConstructBaseManagementVMAsync();
            return View("Index", model);
        }
    }
}

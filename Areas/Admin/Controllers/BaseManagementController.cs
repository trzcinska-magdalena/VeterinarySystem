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

        private void SetTempData(string tempType, string tempData)
        {
            TempData[tempType] = tempData;
        }

        private void SetLogError(Exception ex)
        {
            //TODO
        }

        private void SetErrorViewBag()
        {
            ViewBag.Error = "true";
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await ConstructBaseManagementVMAsync();
                return View(model);
            }
            catch (Exception ex)
            {
                SetLogError(ex);
                SetErrorViewBag();
                return View();
            }
        }

        private async Task SaveBreedInTheBaseAsync(Breed breed)
        {
            _unitOfWork.Breeds.Add(breed);
            await _unitOfWork.SaveAsync();
        }

        private async Task UpdateBreedInTheBaseAsync(Breed breed)
        {
            _unitOfWork.Breeds.Update(breed);
            await _unitOfWork.SaveAsync();
        }

        private async Task RemoveBreedInTheBaseAsync(Breed breed)
        {
            _unitOfWork.Breeds.Remove(breed);
            await _unitOfWork.SaveAsync();
        }

        private async Task<Breed?> GetBreedOrNullAsync(Expression<Func<Breed, bool>> filter)
        {
                return await _unitOfWork.Breeds.GetAsync(filter);
        }

        private async Task<bool> BreedExistsAsync(Expression<Func<Breed, bool>> filter)
        {
            return await _unitOfWork.Breeds.BreedExistsAsync(filter);
        }
     

        [HttpPost, ActionName("AddNewBreed")]
        public async Task<IActionResult> AddNewBreedAsync(Breed newBreed)
        {
            try
            {
                if (!await BreedExistsAsync(e => e.Name == newBreed.Name))
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
            catch (Exception ex)
            {
                SetLogError(ex);
                SetErrorViewBag();
                return View();
            }          
        }

        [HttpPost, ActionName("UpdateBreed")]
        public async Task<IActionResult> UpdateBreedAsync(Breed newBreed)
        {
            try
            {
                if (await BreedExistsAsync(e => e.Name == newBreed.Name && e.Id != newBreed.Id))
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
            catch (Exception ex)
            {
                SetLogError(ex);
                SetErrorViewBag();
                return View();
            }  
        }

        [HttpPost, ActionName("DeleteBreed")]
        public async Task<IActionResult> DeleteBreedAsync(int id)
        {
            try
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
            catch (Exception ex)
            {
                SetLogError(ex);
                SetErrorViewBag();
                return View();
            }
        }
    }
}

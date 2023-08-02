using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq.Expressions;
using VeterinarySystem.Data;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Models.ViewModels;
using VeterinarySystem.Repository;
using VeterinarySystem.Repository.IRepository;
using VeterinarySystem.Service;

namespace VeterinarySystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRole.Role_Admin)]
    public class BaseManagementController : Controller
    {
        private readonly ISystemService _systemService;
      

        public BaseManagementController(ISystemService systemService)
        {
            _systemService = systemService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await _systemService.ConstructBaseManagementVMAsync();
                return View(model);
            }
            catch (Exception ex)
            {
                _systemService.SetLogError(ex);
                return RedirectToAction("Index", "Error");
            }
        }

        
        [HttpPost, ActionName("AddNewBreed")]
        public async Task<IActionResult> AddNewBreedAsync(Breed newBreed)
        {
            try
            {
                if (await _systemService.BreedExistsAsync(e => e.Name == newBreed.Name))
                {
                    _systemService.SetTempData(TempData, "error", "This breed exists!");
                    _systemService.SetLogInfo("");
                }
                else if (ModelState.IsValid)
                {
                    await _systemService.SaveBreedInTheBaseAsync(newBreed);
                    _systemService.SetTempData(TempData, "success", "The breed was created successfully");
                    _systemService.SetLogInfo("");

                    return RedirectToAction("Index");
                }

                var model = await _systemService.ConstructBaseManagementVMAsync();
                return View("Index", model);
            }
            catch (Exception ex)
            {
                _systemService.SetLogError(ex);
                return RedirectToAction("Index", "Error");
            }          
        }

        [HttpPost, ActionName("UpdateBreed")]
        public async Task<IActionResult> UpdateBreedAsync(Breed newBreed)
        {
            try
            {
                if (await _systemService.BreedExistsAsync(e => e.Name == newBreed.Name && e.Id != newBreed.Id))
                {
                    _systemService.SetTempData(TempData, "error", "This breed exists!");
                    _systemService.SetLogInfo("");
                }
                else if (ModelState.IsValid)
                {
                    var breedToUpdate = await _systemService.GetBreedOrNullAsync(e => e.Id == newBreed.Id && e.Name != newBreed.Name);
                    if (breedToUpdate != null)
                    {
                        breedToUpdate.Name = newBreed.Name;

                        await _systemService.UpdateBreedInTheBaseAsync(breedToUpdate);
                        _systemService.SetTempData(TempData, "success", "The breed was updated successfully");
                        _systemService.SetLogInfo("");

                        return RedirectToAction("Index");
                    }
                }
                var model = await _systemService.ConstructBaseManagementVMAsync();
                return View("Index", model);
            }
            catch (Exception ex)
            {
                _systemService.SetLogError(ex);
                return RedirectToAction("Index", "Error");
            }  
        }
    }
}

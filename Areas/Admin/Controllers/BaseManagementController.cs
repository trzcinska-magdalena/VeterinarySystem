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
using VeterinarySystem.Service.IService;

namespace VeterinarySystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRole.Role_Admin)]
    public class BaseManagementController : Controller
    {
        private readonly IBaseManagementService _baseManagement;
        private readonly IUnitOfWork _unitOfWork;

        public BaseManagementController(IBaseManagementService baseManagement, IUnitOfWork unitOfWork)
        {
            _baseManagement = baseManagement;
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
                var model = await _baseManagement.ConstructBaseManagementVMAsync();
                return View(model);
            }
            catch (Exception ex)
            {
                _baseManagement.SetLogError(ex);
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost, ActionName("AddNewBreed")]
        public async Task<IActionResult> AddNewBreedAsync(Breed newBreed)
        {
            try
            {
                if (ModelState.IsValid && await _baseManagement.AddNewBreedAsync(newBreed))
                {
                    SetTempData("success", "The breed was created successfully.");
                    return RedirectToAction("Index");
                }

                SetTempData("error", "This breed exists!");
                var model = await _baseManagement.ConstructBaseManagementVMAsync();
                return View("Index", model);
            }
            catch (Exception ex)
            {
                //_systemService.SetLogError(ex);
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost, ActionName("UpdateBreed")]
        public async Task<IActionResult> UpdateBreedAsync(Breed newBreed)
        {
            try
            {
                (bool success, string message) = await _baseManagement.UpdateBreedAsync(newBreed);
                if (ModelState.IsValid && success)
                {
                    SetTempData("success", "The breed was updated successfully.");
                    return RedirectToAction("Index");
                }

                SetTempData("error", message);
                var model = await _baseManagement.ConstructBaseManagementVMAsync();
                //return View("Index", model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //_systemService.SetLogError(ex);
                return RedirectToAction("Index", "Error");
            }
        }
    }
}

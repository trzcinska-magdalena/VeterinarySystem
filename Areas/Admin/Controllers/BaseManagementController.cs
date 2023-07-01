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
                Accounts = _unitOfWork.Accounts.GetAll()
            };
            return baseManagementViewModel;
        }
        public IActionResult Index(string type)
        {
            return View(ConstructBaseManagementVM());
        }

        [HttpPost]
        public IActionResult AddNewAccount(Account newAccount)
        {
            if (_unitOfWork.Accounts.GetAll().Any(e => e.Login == newAccount.Login))
            {
                TempData["error"] = "Login exist!";
                return View("Index", ConstructBaseManagementVM());
            }
            if (!ModelState.IsValid)
            {
                return View("Index", ConstructBaseManagementVM());
            }
            
            _unitOfWork.Accounts.Add(newAccount);
            _unitOfWork.Save();
            TempData["success"] = "Account created successfully";
            return RedirectToAction("Index", new { type = "Account" });
        }

        [HttpPost]
        public IActionResult UpdateAccount(Account newAccount)
        {
            var existingAccount = _unitOfWork.Accounts.GetAll().Where(e => e.Login == newAccount.Login).FirstOrDefault();
            if (existingAccount != null && existingAccount.Id != newAccount.Id)
            {
                TempData["error"] = "Login exist!";
                return View("Index", ConstructBaseManagementVM());
            }
            if (!ModelState.IsValid)
            {
                return View("Index", ConstructBaseManagementVM());
            }

            var account = _unitOfWork.Accounts.Get(e => e.Id == newAccount.Id);
            if (account == null)
            {
                return View("Index", ConstructBaseManagementVM());
            }

            account.Login = newAccount.Login;
            account.Password = newAccount.Password;
            account.Admin = newAccount.Admin;

            _unitOfWork.Accounts.Update(account);
            _unitOfWork.Save();
            TempData["success"] = "Account updated successfully";
            return RedirectToAction("Index", new { type = "Account" });
        }


        [HttpPost, ActionName("DeleteAccount")]
        public IActionResult DeleteAccount(int id)
        {
            var account = _unitOfWork.Accounts.Get(e => e.Id == id);
            if (account == null)
            {
                return View("Index", ConstructBaseManagementVM());
            }

            _unitOfWork.Accounts.Remove(account);
            _unitOfWork.Save();
            TempData["success"] = "Account removed successfully";
            return RedirectToAction("Index", new { type = "Account" });
        }


    }
}

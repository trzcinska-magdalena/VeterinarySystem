using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Repository.IRepository;

namespace VeterinarySystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(Account account)
        {
            var acc= _unitOfWork.Accounts.Get(e => e.Login == account.Login && e.Password == account.Password);

            if (!ModelState.IsValid || acc == null)
            {
                if (acc == null)
                {
                    ModelState.AddModelError("", "Incorrect login or/and password!");

                }
                return View();
            }
            return RedirectToAction("index", "animal");
        }
    }
}

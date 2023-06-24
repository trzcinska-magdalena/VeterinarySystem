using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Repository.IRepository;

namespace VeterinarySystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUnitOfWork _unionOfWork;
        public LoginController(IUnitOfWork unionOfWork)
        {
            _unionOfWork = unionOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(Account account)
        {
            var acc= _unionOfWork.Accounts.Get(e => e.Login == account.Login && e.Password == account.Password);

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

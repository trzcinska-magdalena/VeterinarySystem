using Microsoft.AspNetCore.Mvc;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Repository.IRepository;

namespace VeterinarySystem.Areas.Employee.Controllers
{
    [Area("Employee")]
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
        public IActionResult Index(Account obj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var account = _unitOfWork.Accounts.Get(e => e.Login == obj.Login && e.Password == obj.Password);
            if (account == null)
            {
                ModelState.AddModelError("", "Incorrect login or/and password!");
                return View();
            }
            Console.Write("pop");
            return RedirectToAction("Index", "Animal");
        }
    }
}

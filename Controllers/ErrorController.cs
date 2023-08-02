using Microsoft.AspNetCore.Mvc;

namespace VeterinarySystem.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult Index()
        {

            return View();

        }
    }
}

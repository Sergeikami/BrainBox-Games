using Microsoft.AspNetCore.Mvc;

namespace WebApiProjecte.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

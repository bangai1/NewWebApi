using Microsoft.AspNetCore.Mvc;

namespace NewWebApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult links()
        {
            return View();
        }
    }
}

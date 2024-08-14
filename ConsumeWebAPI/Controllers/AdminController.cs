using Microsoft.AspNetCore.Mvc;

namespace ConsumeWebAPI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

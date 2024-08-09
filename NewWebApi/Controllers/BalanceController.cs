using Microsoft.AspNetCore.Mvc;

namespace NewWebApi.Controllers
{
    public class BalanceController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }
    }
}

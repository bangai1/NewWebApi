using Microsoft.AspNetCore.Mvc;
using NewWebApi.Models;

namespace NewWebApi.Controllers
{
    public class LoginController : Controller
    {
         

        public IActionResult Index()
        {
            return View();
        }
    }
}

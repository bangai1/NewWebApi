using Microsoft.AspNetCore.Mvc;
using ConsumeWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using ConsumeWebAPI.Data;
using Newtonsoft.Json;

namespace ConsumeWebAPI.Controllers
{
    public class AdminController : Controller
    {

        private readonly HttpClient _client;
        

        public AdminController()
        {
            _client = new HttpClient { BaseAddress = new Uri("https://localhost:7170/") };
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            List<AppUser> userlist = new List<AppUser>();

            HttpResponseMessage response = await _client.GetAsync("Admin/GetAllUsers/getAllUsers");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                userlist = JsonConvert.DeserializeObject<List<AppUser>>(data);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                userlist = userlist.FindAll(u => u.UserName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            return View(userlist);
        }





    }
}

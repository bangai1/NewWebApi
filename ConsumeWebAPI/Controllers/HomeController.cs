using ConsumeWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ConsumeWebAPI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

       

        private readonly HttpClient _client;

        public HomeController()
        {
            _client = new HttpClient { BaseAddress = new Uri("https://localhost:7170/") };
        }


        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            List<CompanyViewModel> companyList = new List<CompanyViewModel>();

            HttpResponseMessage response = await _client.GetAsync("Company/getCompany");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                companyList = JsonConvert.DeserializeObject<List<CompanyViewModel>>(data);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                companyList = companyList.FindAll(c => c.Company_Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            return View(companyList);
        }

        public IActionResult OnlineUsers()
        {
            return View();
        }


        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Profiles()
        {
            return View();
        }

        




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

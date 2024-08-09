using ConsumeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConsumeWebAPI.Controllers
{
    public class CompanyController : Controller
    {
        private readonly HttpClient _client;

        public CompanyController()
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
    }
}

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
            Models.Content obj;
            List<Models.Content> lobj = new List<Models.Content>();

            obj = new Content();
            obj.title=  "User 1";
            obj.text= "(infos)";
            obj.img= "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRTs7lUKpNI9RWwqUx3h4wmDHs4gPutew7Bug&s";

            lobj.Add(obj);

            obj = new Content();
            obj.title = "User 1";
            obj.text = "(infos)";
            obj.img = "https://external-preview.redd.it/ancx8uloRYb3GUrsMhwzIG58CT8PY0GvKQqLv0cmsRM.jpg?auto=webp&s=312ae0e21d6985678982340853390ac93b162008";

            lobj.Add(obj);


            return View(lobj);
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

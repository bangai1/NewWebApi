using ConsumeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConsumeWebAPI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _client;

        public CustomerController()
        {
            _client = new HttpClient { BaseAddress = new Uri("https://localhost:7170/") };
        }

        [HttpGet]
        public async Task<IActionResult> Index(string cust_no)
        {
            List<CustomerViewModel> customerList = new List<CustomerViewModel>();

            HttpResponseMessage response = await _client.GetAsync("Customer/GetCustomerByCustomNo/{custNo}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                customerList = JsonConvert.DeserializeObject<List<CustomerViewModel>>(data);
            }


            if (!string.IsNullOrEmpty(cust_no))
            {
                customerList = customerList.FindAll(c => c.Cust_No.Contains(cust_no, StringComparison.OrdinalIgnoreCase));
            }

            return View(customerList);
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}




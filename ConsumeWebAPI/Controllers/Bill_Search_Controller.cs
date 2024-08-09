using ConsumeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConsumeWebAPI.Controllers
{
    public class Bill_Search_Controller : Controller
    {
        private readonly HttpClient _client;

        public Bill_Search_Controller()
        {
            _client = new HttpClient { BaseAddress = new Uri("https://localhost:7170/") };
        }


        [HttpGet]
        public async Task<IActionResult> Index(string cust_no)
        {
            List<BillsViewModel> bill_list = new List<BillsViewModel>();

            // Check if cust_no is null or empty
            if (string.IsNullOrEmpty(cust_no))
            {
                ViewBag.ErrorMessage = "Customer number is required.";
                return View(bill_list);
            }

            // Ensure cust_no is a valid integer
            if (!int.TryParse(cust_no, out int custNoInt))
            {
                ViewBag.ErrorMessage = "Invalid customer number format.";
                return View(bill_list);
            }

            HttpResponseMessage response = await _client.GetAsync("Bills/GetBills_by_Bill_No_and_Company_No/{Bill_No}/{Company_No}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                bill_list = JsonConvert.DeserializeObject<List<BillsViewModel>>(data);
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to retrieve data from API.";
                return View(bill_list);
            }

            return View(bill_list);
        }
    }
}

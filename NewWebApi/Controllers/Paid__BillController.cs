using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NewWebApi.Models;
using System.Data;

namespace NewWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Paid__BillController : ControllerBase
    {



        private readonly IConfiguration _configuration;
        private readonly ILogger<Paid__BillController> _logger;

        public Paid__BillController(ILogger<Paid__BillController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }


        private static async Task<IEnumerable<Paid__Bills>> SelectAllPaidBills(SqlConnection connection)
        {
            return await connection.QueryAsync<Paid__Bills>("select * from Paid__Bills");
        }



        [HttpGet("getPaidBills")]
        public async Task<IActionResult> GetAllPaidBills()
        {

            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                IEnumerable<Paid__Bills> paid_bills = await SelectAllPaidBills(connection);
                return Ok(paid_bills);
            }

        }


        [HttpGet("bill_no")]
        public async Task<IActionResult> GetAll_From_subs_no(int bill_no)
        {

            {

                using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                var parameters = new DynamicParameters();
                parameters.Add("@bill_no", bill_no);

                var customers = await connection.QueryAsync<Customer>("  select Bill_No from Paid__Bills");
                return Ok(customers.FirstOrDefault()); ;
            }

        }



    }

}
    


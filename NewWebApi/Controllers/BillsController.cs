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

    public class BillsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CustomerController> _logger;

        public BillsController(ILogger<CustomerController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        // public List<Customer> cList = new List<Customer>();




        private static async Task<IEnumerable<Bills>> SelectAllBills(SqlConnection connection)
        {
            return await connection.QueryAsync<Bills>("select * from Bills");
        }



        [HttpGet("getAllBills")]

        public async Task<IActionResult> GetAllBills()
        {

            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                IEnumerable<Bills> bills = await SelectAllBills(connection);
                return Ok(bills);
            }

        }


        [HttpGet("GetBills_by_Cust_No/{Cust_No}")]
        public async Task<ActionResult<Bills>> GetAllBills_by_CustomerID(string Cust_No)
        {


            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parameters = new DynamicParameters();
            parameters.Add("@Cust_No", Cust_No);

            var bills_to_cust = await connection.QueryAsync<Bills>("SELECT * FROM Bills WHERE Cust_No = @Cust_No", parameters);
            return Ok(bills_to_cust);

        }

        [HttpGet("GetBills_by_Bill_No_and_Company_No/{Bill_No}/{Company_No}")]
        public async Task<ActionResult<Bills>> GetBills_by_Cust_No_and_Company_No(string Bill_No, string Company_No)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parameters = new DynamicParameters();
            parameters.Add("@Bill_No", Bill_No);
            parameters.Add("@Company_No", Company_No);

            var bills = await connection.QueryAsync<Bills>("SELECT * FROM Bills WHERE Bill_No = @Bill_No AND Company_No = @Company_No", parameters);
            return Ok(bills);
        }
    }


        //public async Task<IActionResult> Add_To_PaidBills(Bills bills)
        //{

        //    {
        //        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        //        var parameters = new DynamicParameters();

        //        parameters.Add("@Status", bills.Status);
        //        parameters.Add("@CustNo", bills.Cust_No);
                
        //        //await connection.ExecuteAsync("Update Customer  set Status='@Status' , Balance ='@Balance'  where ID = '@ID' ", parameters);
                
        //        return Ok(await SelectAllBills(connection));

        //    }

        //}







    }


using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NewWebApi.Models;
using System.Data;

namespace NewWebApi.Controllers

{
    [Route("[controller]/[action]")]
    [ApiController]

    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        // public List<Customer> cList = new List<Customer>();




        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {

            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                IEnumerable<Customer> customers = await SelectAllCustomers(connection);
                return Ok(customers);
            }

        }

        [HttpGet("{Cust_No}")]
        public async Task<ActionResult<Customer>> GetCustomerByCustomNo(string Cust_No)
        {


            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parameters = new DynamicParameters();
            parameters.Add("@Cust_No", Cust_No);

            var customers = await connection.QueryAsync<Customer>(" SELECT * FROM Customer WHERE Cust_No = @Cust_No", parameters);
            return Ok(customers.FirstOrDefault());

        }

        private static async Task<IEnumerable<Customer>> SelectAllCustomers(SqlConnection connection)
        {
            return await connection.QueryAsync<Customer>("select * from Customer");
        }


        [HttpPut("Update")]
        public async Task<ActionResult<Customer>> UpdateCustomer(string ID, int Status, float Balance)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parameters = new DynamicParameters();

            parameters.Add("@Status", Status);
            parameters.Add("@Balance", Balance);
            parameters.Add("@ID", ID);

            await connection.ExecuteAsync("Update webdata.dbo.Customer  set Status = @Status, Balance = @Balance  where ID = @ID ", parameters);
            return Ok(await SelectAllCustomers(connection));

        }

        [HttpPost("Create")]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer cus)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parameters = new DynamicParameters();

            parameters.Add("@ID", cus.ID);
            parameters.Add("@Cust_No", cus.Cust_No);
            parameters.Add("@Subs_No", cus.Subs_No);
            parameters.Add("@Name", cus.Name);
            parameters.Add("@Surname", cus.Surname);
            parameters.Add("@Status", cus.Status);
            parameters.Add("@Balance", cus.Balance);



            await connection.ExecuteAsync(" INSERT INTO Customer  (ID , Cust_No, Subs_No, Name  , Surname, Status , Balance) VALUES (@ID , @Cust_No ,@Subs_No , @Name , @Surname , @Status, @Balance )", parameters);
            return Ok(await SelectAllCustomers(connection));

        }


        [HttpDelete("{Cust_ID}")]

        public async Task<ActionResult<Customer>> DeleteCustomer(string Cust_ID)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parameters = new DynamicParameters();
            parameters.Add("@custId", Cust_ID);

            await connection.ExecuteAsync(" DELETE FROM webdata.dbo.Customer WHERE ID= @custId", parameters);
            return Ok(await SelectAllCustomers(connection));

        }


    }
}

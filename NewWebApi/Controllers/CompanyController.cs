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

    public class CompanyController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CustomerController> _logger;

      
        public CompanyController(ILogger<CustomerController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        // public List<Customer> cList = new List<Customer>();


        private static async Task<IEnumerable<Company>> SelectAllCompanies(SqlConnection connection)
        {
            return await connection.QueryAsync<Company>("select * from Company");
        }

        [HttpGet("getCompany")]
        public async Task<IActionResult> GetAllCompaies()
        {

            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                IEnumerable<Company> companies = await SelectAllCompanies(connection);
                return Ok(companies);
            }

        }


        [HttpGet("{company_No}")]
        public async Task<ActionResult<Company>> GetCompanyByID(string company_No)
        {


            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parameters = new DynamicParameters();
            parameters.Add("@company_No", company_No);

            var customers = await connection.QueryAsync<Company>("SELECT * FROM Company WHERE Company_No = @company_No", parameters);
            return Ok(customers.FirstOrDefault());

        }

    }
}

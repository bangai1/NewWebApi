using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewWebApi.Models;
using System.Net;

namespace NewWebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CustomerController> _logger;

        public AdminController(ILogger<CustomerController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        private static async Task<IEnumerable<Users>> SelectALlUsers(SqlConnection connection)
        {
            return await connection.QueryAsync<Users>("  select Id , UserName , Email ,Address ,Name, Surname from AspNetUsers\r\n");
        }


        [HttpGet("getAllUsers")]

        public async Task<IActionResult> GetAllUsers()
        {

            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                IEnumerable<Users> users = await SelectALlUsers(connection);
                return Ok(users);
            }

        }



        [HttpGet("{username}")]

        public async Task<ActionResult<Users>> getUserFromUsername(string username)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parameters = new DynamicParameters();
            parameters.Add("@username", username);

            var users = await connection.QueryAsync<Users>(" select * FROM webdata.dbo.AspNetUsers WHERE UserName= @username", parameters);
            var data = users.FirstOrDefault();
            return Ok(data);

        }

       




        [HttpPut("{username}")]
        public async Task<ActionResult<Users>> UpdateUser(string ıd, string username, string address , string email)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parameters = new DynamicParameters();


            parameters.Add("@ıd", ıd);
            parameters.Add("@username", username);
            parameters.Add("@address", address);
            parameters.Add("@email", email);
            

            await connection.ExecuteAsync("  Update webdata.dbo.AspNetUsers  set UserName = @username ,Address= @address ,Email =@email   where ıd = @Id ", parameters);
            return Ok(await SelectALlUsers(connection));

        }




        [HttpPost("{username}")]
        public async Task<ActionResult<Users>> CreateUser(Users users)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parameters = new DynamicParameters();

            parameters.Add("@ıd", users.Id);
            parameters.Add("@name", users.Name);
            parameters.Add("@surname", users.Surname);
            parameters.Add("@username", users.UserName);
            parameters.Add("@address", users.Address);
            parameters.Add("@email", users.Email);


            await connection.ExecuteAsync("  INSERT INTO AspNetUsers (Id , UserName, Email, Address  , Name, Surname) VALUES (@ıd , @username ,@email, @address , @name , @surname )", parameters);
            return Ok(await SelectALlUsers(connection));

        }




        [HttpDelete("{username}")]

        public async Task<ActionResult<Customer>> DeleteCustomer(string username)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parameters = new DynamicParameters();
            parameters.Add("@username", username);

            await connection.ExecuteAsync(" DELETE FROM webdata.dbo.AspNetUsers WHERE UserName= @username", parameters);
            return Ok(await SelectALlUsers(connection));

        }





    }
}

 # Dotnet Web API Customer Payment App

 ## Preview <br/>

A collection payment website that using swagger ui as an API and handle the 
cutomer payment and questioning operations 

<br/>

## Tools

Visual Studio 2022 <br/>
Dotnet  Version: 8.0.303  <br/>
Swagger UI  <br/>
```
builder.Services.AddSwaggerGen();
```
Dapper version: 2.1.35 <br/>
```
using Dapper;

```

<br/>


## Overview
We have a 2 main project that **ConsumeWebAPI** and **NewWebApi** that 
generate this project.

The **ConsumeWebAPI** for the FrontEnd section that display our website. Using 
HTML , CSS for for view .Also the "onclick" events I used jquerry and ajax for 
response and request operations

The **NewWebApi** for the BackEnd API section.That using swagger ui
to test and make the reques/response operations. It is writtten with
c#.

<br/>

## Structure
The project is based on Database First structure and using Dapper .That I created the DB from
MS SQL Server. After that using connection string to fetching data from my DB.


Connectiong String
```
"ConnectionStrings": {
    "DefaultConnection": "Data Source=DESKTOP-FCAH7OP;Initial Catalog=webdata;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"
  },

```

Initiliazing the model from orginal Customer Table from DB
```
namespace NewWebApi.Models
{
    public class Customer
    {
        public string?  ID { get; set; }
        public int Cust_No { get; set; }

        public int Subs_No { get; set; }
        
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public int Status { get; set; }

        public float Balance { get; set; }


    }
}
```

 Creating configurations
```
      private readonly IConfiguration _configuration;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

```


 Creating configurations
```
      private readonly IConfiguration _configuration;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

```
 Sample (Customer) fetching from SQL

```
  public async Task<IActionResult> GetAllCustomers()
 {

     {
         using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
         IEnumerable<Customer> customers = await SelectAllCustomers(connection);
         return Ok(customers);
     }

 }

```


### (FrontEnd Section) <br/>

**Login page**

//img login 

**Register page**

//img login 

**Account Display**

//img account

**Home page**

-Company selection

-Getting Bill number from customer

-Displaying the current bills the customer by table

-User will see selection table that user can select multiple bills

-Confirm Payment is checking current balance customer and 
amount of the bill 

-Then user is restore in DB






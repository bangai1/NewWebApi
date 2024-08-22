 # Dotnet Web API Customer Payment App

 ## Preview <br/>

A collection payment website that using swagger ui as an API and handle the 
cutomer payment and questioning operations 

<br/>

## Tools

Visual Studio 2022 <br/>
Dotnet  Version: 8.0.303  <br/>
Swagger UI  <br/>
AdminLTE 3.2.0 <br/>
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

<br/>

Connection String
```
"ConnectionStrings": {
    "DefaultConnection": "Data Source=DESKTOP-FCAH7OP;Initial Catalog=webdata;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"
  },

```
<br/>


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

<br/>


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
<br/>


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
<br/>



<br/>
Here is the API(previews) that  I used for the CRUD operations
<br/>

![swagger api](https://github.com/user-attachments/assets/42966040-f504-4497-b9aa-903d1aef6d2b)

<br/>


<br/>
So this our request URL that we are gonna use in our ConsumeAPI section

![repsonse and structure](https://github.com/user-attachments/assets/2197f3c9-d715-4dca-aeb8-8cf6da148190)



<br/>



Here we are consuming our api with http client 
```
  private readonly HttpClient _client;

  public CustomerController()
  {
      _client = new HttpClient { BaseAddress = new Uri("https://localhost:7170/") };
  }
```
<br/>

This the sample get method that after consuming the 
web url from we are adjusting the response message to our website
```
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
```
<br/>


### (FrontEnd Section) <br/>

**Login page**

![log in](https://github.com/user-attachments/assets/9cb0bcf0-c869-4d22-aa05-61a611124a7b)
<br/>


**Register page**

![register](https://github.com/user-attachments/assets/7cc7dd27-ca81-465c-9b27-8ed99b9b27ff)
<br/>

**Dashboard Display**

![admin dashboard](https://github.com/user-attachments/assets/d6c406e6-9a7a-4284-81fa-a40fc9c565eb)
<br/>

**Home page**

![user payment](https://github.com/user-attachments/assets/fcaaae30-3de7-446b-9a27-e6fe0fb99e60)
<br/>


**Company Selection**

//Comapany views
<br/>


-Getting Bill number from customer

-Displaying the current bills the customer by table

-User will see selection table that user can select multiple bills

-Confirm Payment is checking current balance customer and 
amount of the bill 

-Then user is restore in DB






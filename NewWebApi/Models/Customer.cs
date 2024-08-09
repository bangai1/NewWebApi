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

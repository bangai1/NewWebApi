using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConsumeWebAPI.Models
{
    public class CustomerViewModel
    {

        public string? ID { get; set; }
        public string? Cust_No { get; set; }

        public int Subs_No { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public int Status { get; set; }

        public float Balance { get; set; }
    }
}

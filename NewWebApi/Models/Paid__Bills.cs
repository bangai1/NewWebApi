using System.ComponentModel.DataAnnotations;

namespace NewWebApi.Models
{
    public class Paid__Bills
    {

        public string? ID { get; set; }

        public DateTime Created_At { get; set; }

        public int Bill_No { get; set; }

        public float  Amount { get; set; }

        public string?  Paid_By { get; set; }
    }
}

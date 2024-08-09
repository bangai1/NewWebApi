namespace NewWebApi.Models
{
    public class Bills
    {
        public string? ID { get; set; }

        public int Subs_No { get; set; }

        public int Company_No { get; set; }

        public int Cust_No { get; set; }

        public int Status   { get; set; }

        public float Balance { get; set; }
        public DateTime Due_Date { get; set; }



    }
}

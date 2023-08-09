namespace MakeYourTrip.Models.DTO
{
    public class BookDTO
    {
        public int BookId { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? Startdate { get; set; }

        public int? PersonCount { get; set; }
        public string? PackageName { get; set; }
        public string? Region { get; set; }



    }
}

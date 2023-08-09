namespace MakeYourTrip.Models.DTO
{
    public class PostGalleryFormModule
    {
        public int Id { get; set; }

        public int? AdminId { get; set; }

        public string? Adminimage { get; set; }

        public string? ImageType { get; set; }
        public IFormFile? FormFile { get; set; }

    }
}

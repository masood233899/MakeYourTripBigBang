﻿namespace MakeYourTrip.Models.DTO
{
    public class PlaceFormModel
    {
        public int Id { get; set; }

        public int? PackageId { get; set; }

        public int? PlaceId { get; set; }

        public int? DayNumber { get; set; }
        public string? Iterinary { get; set; }


        public IFormFile? FormFile { get; set; }

    }
}

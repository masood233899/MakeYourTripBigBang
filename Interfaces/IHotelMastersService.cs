using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MakeYourTrip.Interfaces
{
    public interface IHotelMastersService
    {
        Task<HotelMaster> Add_HotelMaster(HotelMaster hotelMaster);
        Task<List<HotelMaster>?> View_All_HotelMaster();
        Task<HotelMaster> PostImage([FromForm] HotelFormModule hotelFormModule);


    }
}

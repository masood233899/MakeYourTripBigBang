using MakeYourTrip.Models;

namespace MakeYourTrip.Interfaces
{
    public interface IHotelMastersService
    {
        Task<HotelMaster> Add_HotelMaster(HotelMaster hotelMaster);
        Task<List<HotelMaster>?> View_All_HotelMaster();
    }
}

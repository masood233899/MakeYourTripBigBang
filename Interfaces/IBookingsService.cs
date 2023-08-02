using MakeYourTrip.Models.DTO;
using MakeYourTrip.Models;

namespace MakeYourTrip.Interfaces
{
    public interface IBookingsService
    {
        Task<Booking> Add_Booking(Booking booking);
        Task<Booking?> View_Booking(IdDTO idDTO);
        Task<List<Booking>?> View_All_Bookings();
    }
}

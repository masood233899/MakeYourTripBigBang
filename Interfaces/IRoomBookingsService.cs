using MakeYourTrip.Models;

namespace MakeYourTrip.Interfaces
{
    public interface IRoomBookingsService
    {
        Task<RoomBooking> Add_RoomBooking(RoomBooking roomBooking);
        Task<List<RoomBooking>?> View_All_RoomBookings();
    }
}

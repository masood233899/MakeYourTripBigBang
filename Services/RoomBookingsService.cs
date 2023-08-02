using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Repos;

namespace MakeYourTrip.Services
{
    public class RoomBookingsService: IRoomBookingsService
    {
        private readonly ICrud<RoomBooking, IdDTO> _roomBookingRepo;

        public RoomBookingsService( ICrud<RoomBooking, IdDTO> roomBookingRepo)
        {
            _roomBookingRepo = roomBookingRepo;
        }

        public async Task<RoomBooking> Add_RoomBooking(RoomBooking roomBooking)
        {
            var RoomBookings = await _roomBookingRepo.GetAll();
            var newroombooking = RoomBookings.SingleOrDefault(h => h.Id == roomBooking.Id);
            if (newroombooking == null)
            {
                var myroombooking = await _roomBookingRepo.Add(roomBooking);
                if (myroombooking != null)
                    return myroombooking;
            }
            return null;
        }

        public async Task<List<RoomBooking>?> View_All_RoomBookings()
        {
            var RoomBookings = await _roomBookingRepo.GetAll();
            return RoomBookings;
        }
    }
}

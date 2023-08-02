using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;

namespace MakeYourTrip.Services
{
    public class BookingsService: IBookingsService
    {
        private readonly ICrud<Booking, IdDTO> _bookingsrepo;

        public BookingsService(ICrud<Booking, IdDTO> bookingsrepo)
        {
            _bookingsrepo = bookingsrepo;
        }

        public async Task<Booking> Add_Booking(Booking booking)
        {
            var Bookings = await _bookingsrepo.GetAll();

            var newbookings = Bookings.SingleOrDefault(h => h.Id == booking.Id);
            if (newbookings == null)
            {
                var myBooking = await _bookingsrepo.Add(booking);
                if (myBooking != null)
                    return myBooking;
            }
            return null;
        }

        public async Task<List<Booking>?> View_All_Bookings()
        {
            var Bookings = await _bookingsrepo.GetAll();
            /*if (PlateSizes != null)*/
            return Bookings;
        }

        public async Task<Booking?> View_Booking(IdDTO idDTO)
        {
            var booking = await _bookingsrepo.GetValue(idDTO);
            /*if (booking != null)*/
            return booking;
        }
    }
}

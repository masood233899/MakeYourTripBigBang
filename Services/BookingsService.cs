using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Net;

namespace MakeYourTrip.Services
{
    public class BookingsService: IBookingsService
    {
        private readonly ICrud<Booking, IdDTO> _bookingsrepo;
        private readonly ICrud<PackageMaster, IdDTO> _packageMasterRepo;


        public BookingsService(ICrud<Booking, IdDTO> bookingsrepo, ICrud<PackageMaster, IdDTO> packageMasterRepo)
        {
            _bookingsrepo = bookingsrepo;
            _packageMasterRepo = packageMasterRepo;

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

        public async Task<Booking?> giveFeedback(Booking booking)
        {
            var books = await _bookingsrepo.GetAll();
            var book  = books.SingleOrDefault(b => b.Id == booking.Id);
            if (book == null)
            {
                return null;
            }
            var result = await _bookingsrepo.Update(booking);
            if(result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<List<BookDTO>?> getbookingsbyuserId(IdDTO idDTO)
        {
            var books = await _bookingsrepo.GetAll();
             var package = await _packageMasterRepo.GetAll();
            var result = (from b in books
                         join pm in package on b.PackageMasterId equals pm.Id
                         where b.UserId == idDTO.Idint
                         select new BookDTO
                         {
                            BookId= b.Id,
                             TotalAmount=  b.TotalAmount,
                             Startdate= b.Startdate,
                             PersonCount= b.PersonCount,
                             PackageName =pm.PackageName,
                             Region= pm.Region
                         }).ToList();
            return result;
        }
    }
}

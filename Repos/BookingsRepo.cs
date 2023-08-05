using MakeYourTrip.Exceptions;
using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MakeYourTrip.Repos
{
    public class BookingsRepo: ICrud<Booking, IdDTO>
    {
        private readonly TourPackagesContext _context;
        public BookingsRepo(TourPackagesContext context)
        {
            _context = context;
        }

        public async Task<Booking?> Add(Booking item)
        {
            try
            {
                var newBookings = _context.Bookings.SingleOrDefault(f => f.Id == item.Id);
                if (newBookings == null)
                {
                    await _context.Bookings.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return item;
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }

        public async Task<Booking?> Delete(IdDTO item)
        {
            try
            {
                var Bookings = await _context.Bookings.ToListAsync();
                var Booking = Bookings.FirstOrDefault(h => h.Id == item.Idint);
                if (Booking != null)
                {
                    _context.Bookings.Remove(Booking);
                    await _context.SaveChangesAsync();
                    return Booking;
                }
                else
                    return null;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }

        public async Task<List<Booking>?> GetAll()
        {
            try
            {
                var Bookings = await _context.Bookings.ToListAsync();
                if (Bookings != null)
                    return Bookings;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<Booking?> GetValue(IdDTO item)
        {
            try
            {
                var Bookings = await _context.Bookings.ToListAsync();
                var Booking = Bookings.SingleOrDefault(h => h.Id == item.Idint);
                if (Booking != null)
                    return Booking;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<Booking?> Update(Booking item)
        {
            try
            {
                var Bookings = await _context.Bookings.ToListAsync();
                var Booking = Bookings.SingleOrDefault(h => h.Id == item.Id);
                if (Booking != null)
                {
                    Booking.Feedback = item.Feedback != null ? item.Feedback : Booking.Feedback;
                    Booking.TotalAmount = item.TotalAmount != null ? item.TotalAmount : Booking.TotalAmount;

                    _context.Bookings.Update(Booking);
                    await _context.SaveChangesAsync();
                    return Booking;
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }
    }
}

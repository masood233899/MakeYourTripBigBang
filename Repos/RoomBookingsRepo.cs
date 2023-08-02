using MakeYourTrip.Exceptions;
using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MakeYourTrip.Repos
{
    public class RoomBookingsRepo: ICrud<RoomBooking, IdDTO>
    {
        private readonly TourPackagesContext _context;

        public RoomBookingsRepo(TourPackagesContext context)
        {
            _context = context;
        }

        public async Task<RoomBooking?> Add(RoomBooking item)
        {
            try
            {
                var newroombooking = _context.RoomBookings.SingleOrDefault(f => f.Id == item.Id);
                if (newroombooking == null)
                {
                    await _context.RoomBookings.AddAsync(item);
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

        public async Task<RoomBooking?> Delete(IdDTO item)
        {
            try
            {
                var RoomBookings = await _context.RoomBookings.ToListAsync();
                var RoomBooking = RoomBookings.FirstOrDefault(h => h.Id == item.Idint);
                if (RoomBooking != null)
                {
                    _context.RoomBookings.Remove(RoomBooking);
                    await _context.SaveChangesAsync();
                    return RoomBooking;
                }
                else
                    return null;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }

        public async Task<List<RoomBooking>?> GetAll()
        {
            try
            {
                var RoomBookings = await _context.RoomBookings.ToListAsync();
                if (RoomBookings != null)
                    return RoomBookings;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async  Task<RoomBooking?> GetValue(IdDTO item)
        {
            try
            {
                var RoomBookings = await _context.RoomBookings.ToListAsync();
                var RoomBooking = RoomBookings.SingleOrDefault(h => h.Id == item.Idint);
                if (RoomBooking != null)
                    return RoomBooking;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<RoomBooking?> Update(RoomBooking item)
        {
            try
            {
                var RoomBookings = await _context.RoomBookings.ToListAsync();
                var RoomBooking = RoomBookings.SingleOrDefault(h => h.Id == item.Id);
                if (RoomBooking != null)
                {
                    RoomBooking.RoomDetailsId = item.RoomDetailsId != null ? item.RoomDetailsId : RoomBooking.RoomDetailsId;


                    _context.RoomBookings.Update(RoomBooking);
                    await _context.SaveChangesAsync();
                    return RoomBooking;
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

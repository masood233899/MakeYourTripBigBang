using MakeYourTrip.Exceptions;
using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MakeYourTrip.Repos
{
    public class VehicleBookingsRepo: ICrud<VehicleBooking, IdDTO>
    {
        private readonly TourPackagesContext _context;

        public VehicleBookingsRepo(TourPackagesContext context)
        {
            _context = context;
        }

        public async Task<VehicleBooking?> Add(VehicleBooking item)
        {
            try
            {
                var newVehicleBooking = _context.VehicleBookings.SingleOrDefault(f => f.Id == item.Id);
                if (newVehicleBooking == null)
                {
                    await _context.VehicleBookings.AddAsync(item);
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

        public async Task<VehicleBooking?> Delete(IdDTO item)
        {
            try
            {
                var VehicleBookings = await _context.VehicleBookings.ToListAsync();
                var VehicleBooking = VehicleBookings.FirstOrDefault(h => h.Id == item.Idint);
                if (VehicleBooking != null)
                {
                    _context.VehicleBookings.Remove(VehicleBooking);
                    await _context.SaveChangesAsync();
                    return VehicleBooking;
                }
                else
                    return null;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }

        public async Task<List<VehicleBooking>?> GetAll()
        {
            try
            {
                var VehicleBookings = await _context.VehicleBookings.ToListAsync();
                if (VehicleBookings != null)
                    return VehicleBookings;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<VehicleBooking?> GetValue(IdDTO item)
        {
            try
            {
                var VehicleBookings = await _context.VehicleBookings.ToListAsync();
                var VehicleBooking = VehicleBookings.SingleOrDefault(h => h.Id == item.Idint);
                if (VehicleBooking != null)
                    return VehicleBooking;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<VehicleBooking?> Update(VehicleBooking item)
        {
            try
            {
                var VehicleBookings = await _context.VehicleBookings.ToListAsync();
                var VehicleBooking = VehicleBookings.SingleOrDefault(h => h.Id == item.Id);
                if (VehicleBooking != null)
                {
                    VehicleBooking.VehicleDetailsId = VehicleBooking.VehicleDetailsId != null ? VehicleBooking.VehicleDetailsId : VehicleBooking.VehicleDetailsId;
                   

                    _context.VehicleBookings.Update(VehicleBooking);
                    await _context.SaveChangesAsync();
                    return VehicleBooking;
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

using MakeYourTrip.Exceptions;
using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MakeYourTrip.Repos
{
    public class VehicleMastersRepo: ICrud<VehicleMaster, IdDTO>
    {
        private readonly TourPackagesContext _context;

        public VehicleMastersRepo(TourPackagesContext context)
        {
            _context = context;
        }

        public async Task<VehicleMaster?> Add(VehicleMaster item)
        {
            try
            {
                var newVehicleMasters = _context.VehicleMasters.SingleOrDefault(f => f.Id == item.Id);
                if (newVehicleMasters == null)
                {
                    await _context.VehicleMasters.AddAsync(item);
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

        public async Task<VehicleMaster?> Delete(IdDTO item)
        {
            try
            {
                var VehicleMasters = await _context.VehicleMasters.ToListAsync();
                var VehicleMaster = VehicleMasters.FirstOrDefault(h => h.Id == item.Idint);
                if (VehicleMaster != null)
                {
                    _context.VehicleMasters.Remove(VehicleMaster);
                    await _context.SaveChangesAsync();
                    return VehicleMaster;
                }
                else
                    return null;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }

        public async Task<List<VehicleMaster>?> GetAll()
        {
            try
            {
                var VehicleMasters = await _context.VehicleMasters.ToListAsync();
                if (VehicleMasters != null)
                    return VehicleMasters;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<VehicleMaster?> GetValue(IdDTO item)
        {
            try
            {
                var VehicleMasters = await _context.VehicleMasters.ToListAsync();
                var VehicleMaster = VehicleMasters.SingleOrDefault(h => h.Id == item.Idint);
                if (VehicleMaster != null)
                    return VehicleMaster;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<VehicleMaster?> Update(VehicleMaster item)
        {
            try
            {
                var VehicleMasters = await _context.VehicleMasters.ToListAsync();
                var VehicleMaster = VehicleMasters.SingleOrDefault(h => h.Id == item.Id);
                if (VehicleMaster != null)
                {
                    VehicleMaster.VehicleName = item.VehicleName != null ? item.VehicleName : VehicleMaster.VehicleName;
                    VehicleMaster.NumberOfSeats = item.NumberOfSeats != null ? item.NumberOfSeats : VehicleMaster.NumberOfSeats;

                    _context.VehicleMasters.Update(VehicleMaster);
                    await _context.SaveChangesAsync();
                    return VehicleMaster;
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

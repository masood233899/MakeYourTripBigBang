using MakeYourTrip.Exceptions;
using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MakeYourTrip.Repos
{
    public class HotelMastersRepo: ICrud<HotelMaster, IdDTO>
    {
        private TourPackagesContext _context;

        public HotelMastersRepo(TourPackagesContext context)
        {
            _context = context;
        }

        public async Task<HotelMaster?> Add(HotelMaster item)
        {
            try
            {
                var newHotel = _context.HotelMasters.SingleOrDefault(f => f.Id == item.Id);
                if (newHotel == null)
                {
                    await _context.HotelMasters.AddAsync(item);
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

        public async Task<HotelMaster?> Delete(IdDTO item)
        {
            try
            {
                var HotelMasters = await _context.HotelMasters.ToListAsync();
                var HotelMaster = HotelMasters.FirstOrDefault(h => h.Id == item.Idint);
                if (HotelMaster != null)
                {
                    _context.HotelMasters.Remove(HotelMaster);
                    await _context.SaveChangesAsync();
                    return HotelMaster;
                }
                else
                    return null;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }

        public async Task<List<HotelMaster>?> GetAll()
        {
            try
            {
                var HotelMasters = await _context.HotelMasters.ToListAsync();
                if (HotelMasters != null)
                    return HotelMasters;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<HotelMaster?> GetValue(IdDTO item)
        {
            try
            {
                var HotelMasters = await _context.HotelMasters.ToListAsync();
                var HotelMaster = HotelMasters.SingleOrDefault(h => h.Id == item.Idint);
                if (HotelMaster != null)
                    return HotelMaster;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<HotelMaster?> Update(HotelMaster item)
        {
            try
            {
                var HotelMasters = await _context.HotelMasters.ToListAsync();
                var HotelMaster = HotelMasters.SingleOrDefault(h => h.Id == item.Id);
                if (HotelMaster != null)
                {
                    HotelMaster.HotelName = HotelMaster.HotelName != null ? HotelMaster.HotelName : HotelMaster.HotelName;
                    HotelMaster.Place = HotelMaster.Place != null ? HotelMaster.Place : HotelMaster.Place;

                    _context.HotelMasters.Update(HotelMaster);
                    await _context.SaveChangesAsync();
                    return HotelMaster;
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

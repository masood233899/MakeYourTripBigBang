using MakeYourTrip.Exceptions;
using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MakeYourTrip.Repos
{
    public class RoomTypeMastersRepo : ICrud<RoomTypeMaster, IdDTO>
    {
        private readonly TourPackagesContext _context;

        public RoomTypeMastersRepo(TourPackagesContext context)
        {
            _context = context;
        }
        public async Task<RoomTypeMaster?> Add(RoomTypeMaster item)
        {
            try
            {
                var newroomtype = _context.RoomTypeMasters.SingleOrDefault(f => f.Id == item.Id);
                if (newroomtype == null)
                {
                    await _context.RoomTypeMasters.AddAsync(item);
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

        public async Task<RoomTypeMaster?> Delete(IdDTO item)
        {
            try
            {
                var RoomTypeMasters = await _context.RoomTypeMasters.ToListAsync();
                var RoomTypeMaster = RoomTypeMasters.FirstOrDefault(h => h.Id == item.Idint);
                if (RoomTypeMaster != null)
                {
                    _context.RoomTypeMasters.Remove(RoomTypeMaster);
                    await _context.SaveChangesAsync();
                    return RoomTypeMaster;
                }
                else
                    return null;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }

        public async Task<List<RoomTypeMaster>?> GetAll()
        {
            try
            {
                var RoomTypeMasters = await _context.RoomTypeMasters.ToListAsync();
                if (RoomTypeMasters != null)
                    return RoomTypeMasters;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<RoomTypeMaster?> GetValue(IdDTO item)
        {
            try
            {
                var RoomTypeMasters = await _context.RoomTypeMasters.ToListAsync();
                var RoomTypeMaster = RoomTypeMasters.SingleOrDefault(h => h.Id == item.Idint);
                if (RoomTypeMaster != null)
                    return RoomTypeMaster;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<RoomTypeMaster?> Update(RoomTypeMaster item)
        {
            try
            {
                var RoomTypeMasters = await _context.RoomTypeMasters.ToListAsync();
                var RoomTypeMaster = RoomTypeMasters.SingleOrDefault(h => h.Id == item.Id);
                if (RoomTypeMaster != null)
                {
                    RoomTypeMaster.RoomType = item.RoomType != null ? item.RoomType : RoomTypeMaster.RoomType;
                    

                    _context.RoomTypeMasters.Update(RoomTypeMaster);
                    await _context.SaveChangesAsync();
                    return RoomTypeMaster;
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

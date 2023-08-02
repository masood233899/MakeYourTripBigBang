using MakeYourTrip.Exceptions;
using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MakeYourTrip.Repos
{
    public class RoomDetailsMastersRepo: ICrud<RoomDetailsMaster, IdDTO>
    {
        private readonly TourPackagesContext _context;

        public RoomDetailsMastersRepo(TourPackagesContext context)
        {
            _context = context;
        }

        public async Task<RoomDetailsMaster?> Add(RoomDetailsMaster item)
        {
            try
            {
                var newroomdetails = _context.RoomDetailsMasters.SingleOrDefault(f => f.Id == item.Id);
                if (newroomdetails == null)
                {
                    await _context.RoomDetailsMasters.AddAsync(item);
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

        public async Task<RoomDetailsMaster?> Delete(IdDTO item)
        {
            try
            {
                var RoomDetailsMasters = await _context.RoomDetailsMasters.ToListAsync();
                var RoomDetailsMaster = RoomDetailsMasters.FirstOrDefault(h => h.Id == item.Idint);
                if (RoomDetailsMaster != null)
                {
                    _context.RoomDetailsMasters.Remove(RoomDetailsMaster);
                    await _context.SaveChangesAsync();
                    return RoomDetailsMaster;
                }
                else
                    return null;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }

        public async Task<List<RoomDetailsMaster>?> GetAll()
        {
            try
            {
                var RoomDetailsMasters = await _context.RoomDetailsMasters.ToListAsync();
                if (RoomDetailsMasters != null)
                    return RoomDetailsMasters;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<RoomDetailsMaster?> GetValue(IdDTO item)
        {
            try
            {
                var RoomDetailsMasters = await _context.RoomDetailsMasters.ToListAsync();
                var RoomDetailsMaster = RoomDetailsMasters.SingleOrDefault(h => h.Id == item.Idint);
                if (RoomDetailsMaster != null)
                    return RoomDetailsMaster;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<RoomDetailsMaster?> Update(RoomDetailsMaster item)
        {
            try
            {
                var RoomDetailsMasters = await _context.RoomDetailsMasters.ToListAsync();
                var RoomDetailsMaster = RoomDetailsMasters.SingleOrDefault(h => h.Id == item.Id);
                if (RoomDetailsMaster != null)
                {
                    RoomDetailsMaster.RoomType = item.RoomType != null ? item.RoomType : RoomDetailsMaster.RoomType;


                    _context.RoomDetailsMasters.Update(RoomDetailsMaster);
                    await _context.SaveChangesAsync();
                    return RoomDetailsMaster;
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

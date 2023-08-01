using MakeYourTrip.Exceptions;
using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace MakeYourTrip.Repos
{
    public class PlaceMastersRepo: ICrud<PlaceMaster, IdDTO>

    {
        private readonly TourPackagesContext _context;

        public PlaceMastersRepo(TourPackagesContext context)
        {
            _context = context;
        }

        public async Task<PlaceMaster?> Add(PlaceMaster placeMaster)
        {
            try
            {
                var newplacemaster = _context.PlaceMasters.SingleOrDefault(f => f.Id == placeMaster.Id);
                if (newplacemaster == null)
                {
                    await _context.PlaceMasters.AddAsync(placeMaster);
                    await _context.SaveChangesAsync();
                    return placeMaster;
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        public async Task<PlaceMaster?> Delete(IdDTO id)
        {
            try
            {
                var placemaster = await _context.PlaceMasters.ToListAsync();
                var myplaceMaster = placemaster.FirstOrDefault(f => f.Id == id.Idint);
                if (myplaceMaster != null)
                {
                    _context.PlaceMasters.Remove(myplaceMaster);
                    await _context.SaveChangesAsync();
                    return myplaceMaster;
                }
                else
                    return null;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        public async Task<List<PlaceMaster>?> GetAll()
        {
            try
            {
                var placeMasters = await _context.PlaceMasters.ToListAsync();
                if (placeMasters != null)
                    return placeMasters;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<PlaceMaster?> GetValue(IdDTO id)
        {
            try
            {
                var placeMasters = await _context.PlaceMasters.ToListAsync();
                var placeMaster = placeMasters.SingleOrDefault(h => h.Id == id.Idint);
                if (placeMaster != null)
                    return placeMaster;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<PlaceMaster?> Update(PlaceMaster item)
        {
            try
            {
                var placeMasters = await _context.PlaceMasters.ToListAsync();
                var placeMaster = placeMasters.SingleOrDefault(h => h.Id == item.Id);
                if (placeMaster != null)
                {
                    placeMaster.PlaceName = item.PlaceName != null ? item.PlaceName : placeMaster.PlaceName;
                    

                    _context.PlaceMasters.Update(placeMaster);
                    await _context.SaveChangesAsync();
                    return placeMaster;
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

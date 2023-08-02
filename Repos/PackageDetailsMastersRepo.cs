using MakeYourTrip.Exceptions;
using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MakeYourTrip.Repos
{
    public class PackageDetailsMastersRepo: ICrud<PackageDetailsMaster, IdDTO>
    {
        private readonly TourPackagesContext _context;
        public PackageDetailsMastersRepo(TourPackagesContext context)
        {
            _context = context;
        }

        public async Task<PackageDetailsMaster?> Add(PackageDetailsMaster item)
        {
            try
            {
                var newPackageDetails = _context.PackageDetailsMasters.SingleOrDefault(f => f.Id == item.Id);
                if (newPackageDetails == null)
                {
                    await _context.PackageDetailsMasters.AddAsync(item);
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

        public async Task<PackageDetailsMaster?> Delete(IdDTO item)
        {
            try
            {
                var Packages = await _context.PackageDetailsMasters.ToListAsync();
                var package = Packages.FirstOrDefault(h => h.Id == item.Idint);
                if (package != null)
                {
                    _context.PackageDetailsMasters.Remove(package);
                    await _context.SaveChangesAsync();
                    return package;
                }
                else
                    return null;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }

        public async Task<List<PackageDetailsMaster>?> GetAll()
        {
            try
            {
                var packages = await _context.PackageDetailsMasters.ToListAsync();
                if (packages != null)
                    return packages;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<PackageDetailsMaster?> GetValue(IdDTO item)
        {
            try
            {
                var packages = await _context.PackageDetailsMasters.ToListAsync();
                var package = packages.SingleOrDefault(h => h.Id == item.Idint);
                if (package != null)
                    return package;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<PackageDetailsMaster?> Update(PackageDetailsMaster item)
        {
            try
            {
                var packages = await _context.PackageDetailsMasters.ToListAsync();
                var package = packages.SingleOrDefault(h => h.Id == item.Id);
                if (package != null)
                {
                    package.PlaceId = package.PlaceId != null ? package.PlaceId : package.PlaceId;
                    package.PackageId = package.PackageId != null ? package.PackageId : package.PackageId;
                    package.DayNumber = package.DayNumber != null ? package.DayNumber : package.DayNumber;

                    _context.PackageDetailsMasters.Update(package);
                    await _context.SaveChangesAsync();
                    return package;
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

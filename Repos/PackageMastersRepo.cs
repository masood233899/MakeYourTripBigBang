using MakeYourTrip.Exceptions;
using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MakeYourTrip.Repos
{
    public class PackageMastersRepo: ICrud<PackageMaster, IdDTO>
    {
        private readonly TourPackagesContext _context;

        public PackageMastersRepo(TourPackagesContext context)
        {
            _context = context;
        }

        public async Task<PackageMaster?> Add(PackageMaster item)
        {
            try
            {
                var newPackage = _context.PackageMasters.SingleOrDefault(f => f.Id == item.Id);
                if (newPackage == null)
                {
                    await _context.PackageMasters.AddAsync(item);
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

        public async Task<PackageMaster?> Delete(IdDTO item)
        {
            try
            {
                var Packages = await _context.PackageMasters.ToListAsync();
                var package = Packages.FirstOrDefault(h => h.Id == item.Idint);
                if (package != null)
                {
                    _context.PackageMasters.Remove(package);
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

        public async Task<List<PackageMaster>?> GetAll()
        {
            try
            {
                var packages = await _context.PackageMasters.ToListAsync();
                if (packages != null)
                    return packages;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<PackageMaster?> GetValue(IdDTO item)
        {
            try
            {
                var packages = await _context.PackageMasters.ToListAsync();
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

        public async Task<PackageMaster?> Update(PackageMaster item)
        {
            try
            {
                var packages = await _context.PackageMasters.ToListAsync();
                var package = packages.SingleOrDefault(h => h.Id == item.Id);
                if (package != null)
                {
                    package.PackageName = package.PackageName != null ? package.PackageName : package.PackageName;
                    package.PackagePrice = package.PackagePrice != null ? package.PackagePrice : package.PackagePrice;
                    package.Region = package.Region != null ? package.Region : package.Region;
                    package.TravelAgentId = package.TravelAgentId != null ? package.TravelAgentId : package.TravelAgentId;

                    _context.PackageMasters.Update(package);
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

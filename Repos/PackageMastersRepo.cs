using MakeYourTrip.Exceptions;
using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MakeYourTrip.Repos
{
    public class PackageMastersRepo: ICrud<PackageMaster, IdDTO>, IImageRepo<PackageMaster, PackageFormModel>
    {
        private readonly TourPackagesContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PackageMastersRepo(TourPackagesContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;

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
                    package.PackageName = item.PackageName != null ? item.PackageName : package.PackageName;
                    package.PackagePrice = item.PackagePrice != null ? item.PackagePrice : package.PackagePrice;
                    package.Region = item.Region != null ? item.Region : package.Region;
                    package.Daysno = item.Daysno != null ? item.Daysno : package.Daysno;
                    package.TravelAgentId = item.TravelAgentId != null ? item.TravelAgentId : package.TravelAgentId;

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
        public async Task<PackageMaster> PostImage([FromForm] PackageFormModel packageFormModel)
        {
            if (packageFormModel == null)
            {
                throw new ArgumentException("Invalid file");
            }

            packageFormModel.Imagepath = await SaveImage(packageFormModel.FormFile);
            var newPackageMaster = new PackageMaster();
            newPackageMaster.PackagePrice = packageFormModel.PackagePrice;
            newPackageMaster.PackageName = packageFormModel.PackageName;
            newPackageMaster.TravelAgentId = packageFormModel.TravelAgentId;
            newPackageMaster.Region = packageFormModel.Region;
            newPackageMaster.Daysno = packageFormModel.Dayno;
            newPackageMaster.PackageImages = packageFormModel.Imagepath;


            _context.PackageMasters.Add(newPackageMaster);
            await _context.SaveChangesAsync();
            return newPackageMaster;
        }


        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot/Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
    }
}

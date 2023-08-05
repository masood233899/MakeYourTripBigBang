using MakeYourTrip.Exceptions;
using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MakeYourTrip.Repos
{
    public class PackageDetailsMastersRepo: ICrud<PackageDetailsMaster, IdDTO>, IImageRepo<PackageDetailsMaster, PlaceFormModel>
    {
        private readonly TourPackagesContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PackageDetailsMastersRepo(TourPackagesContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
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
        public async Task<PackageDetailsMaster> PostImage([FromForm] PlaceFormModel placeFormModel)
        {
            if (placeFormModel == null)
            {
                throw new ArgumentException("Invalid file");
            }

            string PlaceImagepath = await SaveImage(placeFormModel.FormFile);
            var pack = new PackageDetailsMaster();
            pack.PackageId = placeFormModel.PackageId;
            pack.PlaceId = placeFormModel.PlaceId;
            pack.DayNumber = placeFormModel.DayNumber;
            pack.PlaceImages = PlaceImagepath;
            _context.PackageDetailsMasters.Add(pack);
            await _context.SaveChangesAsync();
            return pack;
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

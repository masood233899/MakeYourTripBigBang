using MakeYourTrip.Exceptions;
using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace MakeYourTrip.Repos
{
    public class HotelMastersRepo: ICrud<HotelMaster, IdDTO>, IImageRepo<HotelMaster, HotelFormModule>
    {
        private readonly TourPackagesContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;


        public HotelMastersRepo(TourPackagesContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
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
        public async Task<HotelMaster> PostImage([FromForm] HotelFormModule hotelFormModule)
        {
            if (hotelFormModule == null)
            {
                throw new ArgumentException("Invalid file");
            }

            string HotelImagepath1 = await SaveImage(hotelFormModule.FormFile);
            var hotel = new HotelMaster();
            hotel.HotelName = hotelFormModule.HotelName;
            hotel.PlaceId = hotelFormModule.PlaceId;
            hotel.HotelImages = HotelImagepath1;
            _context.HotelMasters.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
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

using MakeYourTrip.Exceptions;
using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MakeYourTrip.Repos
{
    public class PostGalleryRepo : ICrud<PostGallery, IdDTO>, IImageRepo<PostGallery, PostGalleryFormModule>
    {
        private readonly TourPackagesContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;


        public PostGalleryRepo(TourPackagesContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<PostGallery?> Add(PostGallery item)
        {
            /* try
             {*/
            var newPostGallery = _context.PostGalleries.SingleOrDefault(h => h.Id == item.Id);
            if (newPostGallery == null)
            {
                await _context.PostGalleries.AddAsync(item);
                await _context.SaveChangesAsync();
                return item;
            }

            return null;
            /* }*/
            /*catch (SqlException se)
            {
                throw new InvalidSqlException(se.Message);
            }*/
        }

        public async Task<PostGallery?> Delete(IdDTO item)
        {
            try
            {

                var PostGalleries = await _context.PostGalleries.ToListAsync();
                var myPostGallery = PostGalleries.FirstOrDefault(h => h.Id == item.Idint);
                if (myPostGallery != null)
                {
                    _context.PostGalleries.Remove(myPostGallery);
                    await _context.SaveChangesAsync();
                    return myPostGallery;
                }
                else
                    return null;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }

        public async Task<List<PostGallery>?> GetAll()
        {
            try
            {
                var PostGalleries = await _context.PostGalleries.ToListAsync();
                if (PostGalleries != null)
                    return PostGalleries;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<PostGallery?> GetValue(IdDTO item)
        {
            try
            {
                var PostGalleries = await _context.PostGalleries.ToListAsync();
                var PostGallery = PostGalleries.SingleOrDefault(h => h.Id == item.Idint);
                if (PostGallery != null)
                    return PostGallery;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<PostGallery?> Update(PostGallery item)
        {
            try
            {
                var PostGalleries = await _context.PostGalleries.ToListAsync();
                var PostGallery = PostGalleries.SingleOrDefault(h => h.Id == item.Id);
                if (PostGallery != null)
                {
                    PostGallery.AdminId = item.AdminId != null ? item.AdminId : PostGallery.AdminId;
                    PostGallery.Images = item.Images != null ? item.Images : PostGallery.Images;
                    PostGallery.ImageType = item.ImageType != null ? item.ImageType : PostGallery.ImageType;

                    _context.PostGalleries.Update(PostGallery);
                    await _context.SaveChangesAsync();
                    return PostGallery;
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<PostGallery> PostImage([FromForm] PostGalleryFormModule postGalleryFormModule)
        {
            if (postGalleryFormModule == null)
            {
                throw new ArgumentException("Invalid file");
            }

            postGalleryFormModule.Adminimage = await SaveImage(postGalleryFormModule.FormFile);
            var newPostGallery = new PostGallery();
            newPostGallery.AdminId = postGalleryFormModule.AdminId;
            newPostGallery.ImageType = postGalleryFormModule.ImageType;

            newPostGallery.Images = postGalleryFormModule.Adminimage;


            _context.PostGalleries.Add(newPostGallery);
            await _context.SaveChangesAsync();
            return newPostGallery;
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

using MakeYourTrip.Interfaces;
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Models;
using MakeYourTrip.Repos;
using Microsoft.AspNetCore.Mvc;

namespace MakeYourTrip.Services
{
    public class PostGalleryService : IPostGalleryService
    {
        private readonly ICrud<PostGallery, IdDTO> _PostGalleryRepo;
        private readonly IImageRepo<PostGallery, PostGalleryFormModule> _imageRepo;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PostGalleryService(ICrud<PostGallery, IdDTO> postGalleryRepo,
            IImageRepo<PostGallery, PostGalleryFormModule> imageRepo, IWebHostEnvironment hostEnvironment)
        {
            _PostGalleryRepo = postGalleryRepo;
            _imageRepo = imageRepo;
            _hostEnvironment = hostEnvironment;

        }


        public async Task<PostGallery?> Add_PostGallery(PostGallery PostGallery)
        {
            var PostGallerytable = await _PostGalleryRepo.GetAll();
            var newPostGallery = PostGallerytable?.SingleOrDefault(h => h.Id == PostGallery.Id);
            if (newPostGallery == null)
            {
                var myPostGallery = await _PostGalleryRepo.Add(PostGallery);
                if (myPostGallery != null)
                    return myPostGallery;
            }
            return null;

        }
        public async Task<List<PostGallery>?> Get_all_PostGallery()
        {
            var PostGallerys = await _PostGalleryRepo.GetAll();
            var images = await _PostGalleryRepo.GetAll();
            var imageList = new List<PostGallery>();
            foreach (var image in images)
            {
                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                var filePath = Path.Combine(uploadsFolder, image.Images);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);
                var tourData = new PostGallery
                {
                    Id = image.Id,
                    AdminId = image.AdminId,

                    ImageType = image.ImageType,

                    Images = Convert.ToBase64String(imageBytes)
                };
                imageList.Add(tourData);
            }
            return imageList;

        }

        public async Task<PostGallery?> View_PostGallery(IdDTO idDTO)
        {
            var PostGallery = await _PostGalleryRepo.GetValue(idDTO);
            return PostGallery;
        }

        public async Task<PostGallery> PostImage([FromForm] PostGalleryFormModule postGalleryFormModule)
        {
            if (postGalleryFormModule == null)
            {
                throw new Exception("Invalid file");
            }
            var item = await _imageRepo.PostImage(postGalleryFormModule);
            if (item == null)
            {
                return null;
            }
            return item;
        }



    }
}

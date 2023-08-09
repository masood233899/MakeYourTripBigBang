using MakeYourTrip.Models.DTO;
using MakeYourTrip.Models;
using Microsoft.AspNetCore.Mvc;

namespace MakeYourTrip.Interfaces
{
    public interface IPostGalleryService
    {
        Task<PostGallery?> Add_PostGallery(PostGallery PostGallery);

        Task<List<PostGallery>?> Get_all_PostGallery();
        Task<PostGallery?> View_PostGallery(IdDTO idDTO);


        Task<PostGallery> PostImage([FromForm] PostGalleryFormModule postGalleryFormModule);

    }
}

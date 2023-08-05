using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MakeYourTrip.Interfaces
{
    public interface IImageRepo<T, K>
    {
        Task<T> PostImage([FromForm] K formodel);
    }
}

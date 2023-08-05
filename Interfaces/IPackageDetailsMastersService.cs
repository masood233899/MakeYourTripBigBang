using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MakeYourTrip.Interfaces
{
    public interface IPackageDetailsMastersService
    {
        Task<List<PackageDetailsMaster>?> Add_PackageDetailsMaster(List<PackageDetailsMaster> PackageDetailsMaster);
        Task<List<PackageDetailsMaster>?> View_All_PackageDetailsMaster();
        Task<PackageDetailsMaster> PostImage([FromForm] PlaceFormModel placeFormModel);

    }
}

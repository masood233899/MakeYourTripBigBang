using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MakeYourTrip.Interfaces
{
    public interface IPackageMastersService
    {
        Task<PackageMaster> Add_PackageMaster(PackageMaster packageMaster);
        Task<List<PackageMaster>?> View_All_PackageMaster();
        Task<PackageMaster?> View_PackageMaster(IdDTO idDTO);
        Task<PackageMaster> PostDashboardImage([FromForm] PackageFormModel packageFormModel);
        Task<PackageDTO?> Get_package_details(IdDTO id);

    }
}

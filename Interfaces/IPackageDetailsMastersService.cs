using MakeYourTrip.Models;

namespace MakeYourTrip.Interfaces
{
    public interface IPackageDetailsMastersService
    {
        Task<PackageDetailsMaster> Add_PackageDetailsMaster(PackageDetailsMaster packageDetailsMaster);
        Task<List<PackageDetailsMaster>?> View_All_PackageDetailsMaster();
    }
}

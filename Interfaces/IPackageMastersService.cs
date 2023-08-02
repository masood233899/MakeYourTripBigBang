using MakeYourTrip.Models;

namespace MakeYourTrip.Interfaces
{
    public interface IPackageMastersService
    {
        Task<PackageMaster> Add_PackageMaster(PackageMaster packageMaster);
        Task<List<PackageMaster>?> View_All_PackageMaster();
    }
}

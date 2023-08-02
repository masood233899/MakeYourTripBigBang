using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Repos;

namespace MakeYourTrip.Services
{
    public class PackageMastersService: IPackageMastersService
    {
        private readonly ICrud<PackageMaster, IdDTO> _packageMasterRepo;

        public PackageMastersService(ICrud<PackageMaster, IdDTO> PackageMasterRepo)
        {
            _packageMasterRepo = PackageMasterRepo;
        }

        public async Task<PackageMaster> Add_PackageMaster(PackageMaster packageMaster)
        {
            var packages = await _packageMasterRepo.GetAll();
            var newpackage = packages.SingleOrDefault(h => h.Id == packageMaster.Id);
            if (newpackage == null)
            {
                var mypackage = await _packageMasterRepo.Add(packageMaster);
                if (mypackage != null)
                    return mypackage;
            }
            return null;
        }

        public async Task<List<PackageMaster>?> View_All_PackageMaster()
        {
            var packages = await _packageMasterRepo.GetAll();
            return packages;
        }
    }
}

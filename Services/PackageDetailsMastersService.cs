using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Repos;

namespace MakeYourTrip.Services
{
    public class PackageDetailsMastersService: IPackageDetailsMastersService
    {
        private ICrud<PackageDetailsMaster, IdDTO> _packagedetailsrepo;

        public PackageDetailsMastersService(ICrud<PackageDetailsMaster, IdDTO> packagedetailsrepo)
        {
            _packagedetailsrepo = packagedetailsrepo;
        }

        public async Task<PackageDetailsMaster> Add_PackageDetailsMaster(PackageDetailsMaster packageDetailsMaster)
        {
            var packages = await _packagedetailsrepo.GetAll();
            var newpackage = packages.SingleOrDefault(h => h.Id == packageDetailsMaster.Id);
            if (newpackage == null)
            {
                var myhotel = await _packagedetailsrepo.Add(packageDetailsMaster);
                if (myhotel != null)
                    return myhotel;
            }
            return null;
        }

        public async Task<List<PackageDetailsMaster>?> View_All_PackageDetailsMaster()
        {
            var myhotel = await _packagedetailsrepo.GetAll();
            return myhotel;
        }
    }
}

using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Repos;
using Microsoft.AspNetCore.Mvc;

namespace MakeYourTrip.Services
{
    public class PackageDetailsMastersService: IPackageDetailsMastersService
    {
        private ICrud<PackageDetailsMaster, IdDTO> _packagedetailsrepo;
        private readonly IImageRepo<PackageDetailsMaster, PlaceFormModel> _imageRepo;

        public PackageDetailsMastersService(ICrud<PackageDetailsMaster, IdDTO> packagedetailsrepo, IImageRepo<PackageDetailsMaster, PlaceFormModel> imageRepo)
        {
            _packagedetailsrepo = packagedetailsrepo;
            _imageRepo = imageRepo;

        }

        public async Task<List<PackageDetailsMaster>?> Add_PackageDetailsMaster(List<PackageDetailsMaster> PackageDetailsMaster)
        {

            List<PackageDetailsMaster> addedPackageDetailsMaster = new List<PackageDetailsMaster>();

            var PackageDetailsMasters = await _packagedetailsrepo.GetAll();

            foreach (var packageDetailsMaster in PackageDetailsMaster)
            {

                Console.WriteLine(packageDetailsMaster);

                var myPackageDetailsMaster = await _packagedetailsrepo.Add(packageDetailsMaster);

                if (myPackageDetailsMaster != null)
                {
                    addedPackageDetailsMaster.Add(myPackageDetailsMaster);
                }

            }
            return addedPackageDetailsMaster;

        }

        public async Task<List<PackageDetailsMaster>?> View_All_PackageDetailsMaster()
        {
            var mypackage = await _packagedetailsrepo.GetAll();
            return mypackage;
        }
        public async Task<PackageDetailsMaster> PostImage([FromForm] PlaceFormModel placeFormModel)
        {
            if (placeFormModel == null)
            {
                throw new Exception("Invalid file");
            }
            var item = await _imageRepo.PostImage(placeFormModel);
            if (item == null)
            {
                return null;
            }
            return item;
        }
    }
}

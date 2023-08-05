using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MakeYourTrip.Services
{
    public class VehicleDetailsMasterService: IVehicleDetailsMasterService
    {
        private readonly ICrud<VehicleDetailsMaster, IdDTO> _vehicleDetailsMasterRepo;
        private readonly IImageRepo<VehicleDetailsMaster, VehicleFormModel> _imageRepo;

        public VehicleDetailsMasterService(ICrud<VehicleDetailsMaster, IdDTO> vehicleDetailsMasterRepo, IImageRepo<VehicleDetailsMaster, VehicleFormModel> imageRepo)
        {
            _vehicleDetailsMasterRepo = vehicleDetailsMasterRepo;
            _imageRepo = imageRepo;

        }

        public async Task<List<VehicleDetailsMaster>?> Add_VehicleDetailsMaster(List<VehicleDetailsMaster> VehicleDetailsMaster)
        {

            List<VehicleDetailsMaster> addedVehicleDetailsMaster = new List<VehicleDetailsMaster>();

            var VehicleDetailsMasters = await _vehicleDetailsMasterRepo.GetAll();

            foreach (var vehicleDetailsMaster in VehicleDetailsMaster)
            {

                Console.WriteLine(vehicleDetailsMaster);

                var myVehicleDetailsMaster = await _vehicleDetailsMasterRepo.Add(vehicleDetailsMaster);

                if (myVehicleDetailsMaster != null)
                {
                    addedVehicleDetailsMaster.Add(myVehicleDetailsMaster);
                }

            }
            return addedVehicleDetailsMaster;

        }

        public async Task<List<VehicleDetailsMaster>?> View_All_VehicleDetailsMaster()
        {
            var VehicleDetailsMasters = await _vehicleDetailsMasterRepo.GetAll();
            return VehicleDetailsMasters;
        }
        public async Task<VehicleDetailsMaster> PostImage([FromForm] VehicleFormModel vehicleFormModel)
        {
            if (vehicleFormModel == null)
            {
                throw new Exception("Invalid file");
            }
            var item = await _imageRepo.PostImage(vehicleFormModel);
            if (item == null)
            {
                return null;
            }
            return item;
        }
    }
}

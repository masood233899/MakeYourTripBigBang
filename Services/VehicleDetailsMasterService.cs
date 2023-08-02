using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Repos;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MakeYourTrip.Services
{
    public class VehicleDetailsMasterService: IVehicleDetailsMasterService
    {
        private readonly ICrud<VehicleDetailsMaster, IdDTO> _vehicleDetailsMasterRepo;

        public VehicleDetailsMasterService(ICrud<VehicleDetailsMaster, IdDTO> vehicleDetailsMasterRepo)
        {
            _vehicleDetailsMasterRepo = vehicleDetailsMasterRepo;
        }

        public async Task<VehicleDetailsMaster> Add_VehicleDetailsMaster(VehicleDetailsMaster vehicleDetailsMaster)
        {
            var VehicleDetailsMasters = await _vehicleDetailsMasterRepo.GetAll();
            var newVehicleDetailsMaster = VehicleDetailsMasters.SingleOrDefault(h => h.Id == vehicleDetailsMaster.Id);
            if (newVehicleDetailsMaster == null)
            {
                var myVehicleDetailsMaster = await _vehicleDetailsMasterRepo.Add(vehicleDetailsMaster);
                if (myVehicleDetailsMaster != null)
                    return myVehicleDetailsMaster;
            }
            return null;
        }

        public async Task<List<VehicleDetailsMaster>?> View_All_VehicleDetailsMaster()
        {
            var VehicleDetailsMasters = await _vehicleDetailsMasterRepo.GetAll();
            return VehicleDetailsMasters;
        }
    }
}

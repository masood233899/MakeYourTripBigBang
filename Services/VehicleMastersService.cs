using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Repos;

namespace MakeYourTrip.Services
{
    public class VehicleMastersService: IVehicleMastersService
    {
        private readonly ICrud<VehicleMaster, IdDTO> _vehicleMasterRepo;

        public VehicleMastersService(ICrud<VehicleMaster, IdDTO> vehicleMasterRepo)
        {
            _vehicleMasterRepo = vehicleMasterRepo;
        }

        public async Task<VehicleMaster> Add_VehicleMaster(VehicleMaster vehicleMaster)
        {
            var VehicleMasters = await _vehicleMasterRepo.GetAll();
            var newvehiclemaster = VehicleMasters.SingleOrDefault(h => h.Id == vehicleMaster.Id);
            if (newvehiclemaster == null)
            {
                var myvehicle = await _vehicleMasterRepo.Add(vehicleMaster);
                if (myvehicle != null)
                    return myvehicle;
            }
            return null;
        }

        public async Task<List<VehicleMaster>?> View_All_VehicleMaster()
        {
            var VehicleMaster = await _vehicleMasterRepo.GetAll();
            return VehicleMaster;
        }
    }
}

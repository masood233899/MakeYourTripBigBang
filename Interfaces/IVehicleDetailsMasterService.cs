using MakeYourTrip.Models;

namespace MakeYourTrip.Interfaces
{
    public interface IVehicleDetailsMasterService
    {
        Task<VehicleDetailsMaster> Add_VehicleDetailsMaster(VehicleDetailsMaster vehicleDetailsMaster);
        Task<List<VehicleDetailsMaster>?> View_All_VehicleDetailsMaster();
    }
}

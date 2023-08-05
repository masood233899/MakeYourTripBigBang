using MakeYourTrip.Models;

namespace MakeYourTrip.Interfaces
{
    public interface IVehicleMastersService
    {
        Task<VehicleMaster?> Add_VehicleMaster(VehicleMaster vehicleMaster);
        Task<List<VehicleMaster>?> View_All_VehicleMaster();
    }
}

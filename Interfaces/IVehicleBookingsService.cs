using MakeYourTrip.Models;

namespace MakeYourTrip.Interfaces
{
    public interface IVehicleBookingsService
    {
        Task<VehicleBooking> Add_VehicleBooking(VehicleBooking vehicleBooking);
        Task<List<VehicleBooking>?> View_All_VehicleBooking();
    }
}

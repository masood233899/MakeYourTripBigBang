using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Repos;

namespace MakeYourTrip.Services
{
    public class VehicleBookingsService: IVehicleBookingsService
    {
        private readonly ICrud<VehicleBooking, IdDTO> _vehicleBookingrepo;
        public VehicleBookingsService(ICrud<VehicleBooking, IdDTO> vehicleBookingrepo)
        {
            _vehicleBookingrepo  = vehicleBookingrepo;
        }

        public async Task<VehicleBooking> Add_VehicleBooking(VehicleBooking vehicleBooking)
        {
            var VehicleBookings = await _vehicleBookingrepo.GetAll();
            var newVehicleBooking = VehicleBookings?.SingleOrDefault(h => h.Id == vehicleBooking.Id);
            if (newVehicleBooking == null)
            {
                var myVehicleDetailsMaster = await _vehicleBookingrepo.Add(vehicleBooking);
                if (myVehicleDetailsMaster != null)
                    return myVehicleDetailsMaster;
            }
            return null;
        }

        public async Task<List<VehicleBooking>?> View_All_VehicleBooking()
        {
            var VehicleBookings = await _vehicleBookingrepo.GetAll();
            return VehicleBookings;
        }
        
    }
}

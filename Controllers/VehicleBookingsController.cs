using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeYourTrip.Models;
using MakeYourTrip.Interfaces;
using MakeYourTrip.Exceptions;
using MakeYourTrip.Services;

namespace MakeYourTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleBookingsController : ControllerBase
    {
        private readonly IVehicleBookingsService _vehicleBookingsService;

        public VehicleBookingsController(IVehicleBookingsService vehicleBookingsService)
        {
            _vehicleBookingsService = vehicleBookingsService;
        }

        // GET: api/VehicleBookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleBooking>>> GetVehicleBookings()
        {
            try
            {
                var myVehicle = await _vehicleBookingsService.View_All_VehicleBooking();
                if (myVehicle.Count > 0)
                    return Ok(myVehicle);
                return BadRequest(new Error(10, "No Vehicle Booking Exists"));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }


       
        [HttpPost]
        public async Task<ActionResult<VehicleBooking>> PostVehicleBooking(VehicleBooking vehicleBooking)
        {
            try
            {
                var myVehicle = await _vehicleBookingsService.Add_VehicleBooking(vehicleBooking);
                if (myVehicle.Id != null)
                    return Created("Vehicle Booked Successfully", myVehicle);
                return BadRequest(new Error(1, $"vehicle {myVehicle.Id} is Booked already"));
            }
            catch (InvalidPrimaryKeyId ip)
            {
                return BadRequest(new Error(2, ip.Message));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeYourTrip.Models;
using MakeYourTrip.Exceptions;
using MakeYourTrip.Interfaces;

namespace MakeYourTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleDetailsMastersController : ControllerBase
    {
        private readonly IVehicleDetailsMasterService _vehicleDetailsMasterService;

        public VehicleDetailsMastersController(IVehicleDetailsMasterService vehicleDetailsMasterService)
        {
            _vehicleDetailsMasterService = vehicleDetailsMasterService;
        }

        // GET: api/VehicleDetailsMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDetailsMaster>>> GetVehicleDetailsMasters()
        {
            try
            {
                var myVehicleDetailsMasters = await _vehicleDetailsMasterService.View_All_VehicleDetailsMaster();
                if (myVehicleDetailsMasters.Count > 0)
                    return Ok(myVehicleDetailsMasters);
                return BadRequest(new Error(10, "No Vehicle details Exists"));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<VehicleDetailsMaster>> PostVehicleDetailsMaster(VehicleDetailsMaster vehicleDetailsMaster)
        {
            try
            {
                var myVehicleDetailsMasters = await _vehicleDetailsMasterService.Add_VehicleDetailsMaster(vehicleDetailsMaster);
                if (myVehicleDetailsMasters.Id != null)
                    return Created("Added created Successfully", myVehicleDetailsMasters);
                return BadRequest(new Error(1, $"Vehicle Details {vehicleDetailsMaster.Id} is Present already"));
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

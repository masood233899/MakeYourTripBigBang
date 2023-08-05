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
using MakeYourTrip.Models.DTO;

namespace MakeYourTrip.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VehicleMastersController : ControllerBase
    {
        private readonly IVehicleMastersService _vehicleMastersService;

        public VehicleMastersController(IVehicleMastersService vehicleMastersService)
        {
            _vehicleMastersService = vehicleMastersService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleMaster>>> GetVehicleMasters()
        {
            try
            {
                var myVehicle = await _vehicleMastersService.View_All_VehicleMaster();
                if (myVehicle?.Count > 0)
                    return Ok(myVehicle);
                return BadRequest(new Error(10, "No Vehicles are Existing"));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<VehicleMaster>> PostVehicleMaster(VehicleMaster vehicleMaster)
        {
            try
            {
                var myVehicle = await _vehicleMastersService.Add_VehicleMaster(vehicleMaster);
                if (myVehicle != null)
                    return Created("Vehicle Added Successfully", myVehicle);
                return BadRequest(new Error(1, $"vehicle {vehicleMaster.Id} is Present already"));
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

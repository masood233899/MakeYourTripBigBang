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
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Services;

namespace MakeYourTrip.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PackageDetailsMastersController : ControllerBase
    {
        private readonly IPackageDetailsMastersService _packageDetailsMastersService;

        public PackageDetailsMastersController(IPackageDetailsMastersService packageDetailsMastersService)
        {
            _packageDetailsMastersService = packageDetailsMastersService;
        }

        // GET: api/PackageDetailsMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageDetailsMaster>>> GetPackageDetailsMasters()
        {
            try
            {
                var mypackages = await _packageDetailsMastersService.View_All_PackageDetailsMaster();
                if (mypackages.Count > 0)
                    return Ok(mypackages);
                return BadRequest(new Error(10, "No packages are Existing"));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }

        [ProducesResponseType(typeof(PackageDetailsMaster), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public async Task<ActionResult<List<PackageDetailsMaster>>> Add_PackageDetailsMaster(List<PackageDetailsMaster> PackageDetailsMaster)
        {

            try
            {
                var myPackageDetailsMaster = await _packageDetailsMastersService.Add_PackageDetailsMaster(PackageDetailsMaster);

                if (myPackageDetailsMaster != null)
                {
                    return Created("PackageDetailsMaster created Successfully", myPackageDetailsMaster);
                }

                return BadRequest(new Error(1, "No PackageDetailsMaster were added."));
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
        [ProducesResponseType(typeof(PackageDetailsMaster), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]

        public async Task<ActionResult<PackageDetailsMaster>> PostPlaceMaster([FromForm] PlaceFormModel placeFormModel)
        {
            try
            {
                var createdHotel = await _packageDetailsMastersService.PostImage(placeFormModel);
                return Ok(createdHotel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

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
using static System.Runtime.InteropServices.JavaScript.JSType;
using Error = MakeYourTrip.Models.Error;
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Services;

namespace MakeYourTrip.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlaceMastersController : ControllerBase
    {
        private readonly IPlaceMastersService _placeMastersService;

        public PlaceMastersController(IPlaceMastersService placeMastersService)
        {
            _placeMastersService = placeMastersService;
        }

        [ProducesResponseType(typeof(PlaceMaster), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]
        public async Task<ActionResult<List<PlaceMaster>>> View_All_PlaceMaster()
        {
            try
            {
                var myPlaceMasters = await _placeMastersService.View_All_PlaceMasters();
                if (myPlaceMasters?.Count > 0)
                    return Ok(myPlaceMasters);
                return BadRequest(new Error(10, "No PlateSizes are Existing"));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }

        [ProducesResponseType(typeof(PlaceMaster), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public async Task<ActionResult<PlaceMaster>> Add_PlaceMaster(PlaceMaster PlaceMaster)
        {
            try
            {
                /* if (PlaceMaster.Id <= 0)
                     throw new InvalidPrimaryID();*/
                var myPlaceMaster = await _placeMastersService.Add_PlaceMaster(PlaceMaster);
                if (myPlaceMaster != null)
                    return Created("PlaceMaster created Successfully", myPlaceMaster);
                return BadRequest(new Error(1, $"PlaceMaster {PlaceMaster.Id} is Present already"));
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
        [ProducesResponseType(typeof(PlaceMaster), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]

        public async Task<ActionResult<List<PlaceMaster>>> Get_all_placemaster()
        {
            var myPlaceMasters = await _placeMastersService.View_All_PlaceMasters();
            if (myPlaceMasters?.Count > 0)
                return Ok(myPlaceMasters);
            return BadRequest(new Error(10, "No AdditionalCategoryMasters are Existing"));
        }

        [ProducesResponseType(typeof(PlaceMaster), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpDelete]

        public async Task<ActionResult<PlaceMaster>> Delete_PlaceMaster(IdDTO idDTO)
        {
            try
            {
                if (idDTO.Idint <= 0)
                    return BadRequest(new Error(4, "Enter Valid PlaceMaster ID"));
                var myPlaceMaster = await _placeMastersService.Delete_PlaceMaster(idDTO);
                if (myPlaceMaster != null)
                    return Created("PlaceMaster deleted Successfully", myPlaceMaster);
                return BadRequest(new Error(5, $"There is no PlaceMaster present for the id {idDTO.Idint}"));
            }

            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }
    }
}

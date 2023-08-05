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
    public class RoomDetailsMastersController : ControllerBase
    {
        private readonly IRoomDetailsMastersService _roomDetailsMastersService;

        public RoomDetailsMastersController(IRoomDetailsMastersService roomDetailsMastersService)
        {
            _roomDetailsMastersService = roomDetailsMastersService;
        }

        [ProducesResponseType(typeof(RoomDetailsMaster), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]

        public async Task<ActionResult<List<RoomDetailsMaster>>> Get_all_RoomDetailsMaster()
        {
            var myRoomDetailsMasters = await _roomDetailsMastersService.View_All_RoomDetails();
            if (myRoomDetailsMasters?.Count > 0)
                return Ok(myRoomDetailsMasters);
            return BadRequest(new Error(10, "No RoomDetailsMaster are Existing"));
        }

        [ProducesResponseType(typeof(RoomDetailsMaster), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public async Task<ActionResult<List<RoomDetailsMaster>>> Add_RoomDetailsMaster(List<RoomDetailsMaster> RoomDetailsMaster)
        {

            try
            {
                var myRoomDetailsMaster = await _roomDetailsMastersService.Add_RoomDetailsMaster(RoomDetailsMaster);

                if (myRoomDetailsMaster != null)
                {
                    return Created("RoomDetailsMaster created Successfully", myRoomDetailsMaster);
                }

                return BadRequest(new Error(1, "No RoomDetailsMaster were added."));
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
        [ProducesResponseType(typeof(RoomDetailsMaster), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]

        public async Task<ActionResult<List<RoomdetailsDTO>>> getRoomDetailsByHotel(IdDTO id)
        {
            var myRoomdetails = await _roomDetailsMastersService.getRoomDetailsByHotel(id);
            if (myRoomdetails?.Count > 0)
                return Ok(myRoomdetails);
            return BadRequest(new Error(10, "No RoomDetailsMaster are Existing"));
        }
    }
}

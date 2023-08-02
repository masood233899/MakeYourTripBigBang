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
    public class RoomDetailsMastersController : ControllerBase
    {
        private readonly IRoomDetailsMastersService _roomDetailsMastersService;

        public RoomDetailsMastersController(IRoomDetailsMastersService roomDetailsMastersService)
        {
            _roomDetailsMastersService = roomDetailsMastersService;
        }

        // GET: api/RoomDetailsMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDetailsMaster>>> GetRoomDetailsMasters()
        {
            try
            {
                var myRoomDetail = await _roomDetailsMastersService.View_All_RoomDetails();
                if (myRoomDetail.Count > 0)
                    return Ok(myRoomDetail);
                return BadRequest(new Error(10, "No room types are Existing"));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<RoomDetailsMaster>> PostRoomDetailsMaster(RoomDetailsMaster roomDetailsMaster)
        {
            try
            {
                var myRoomDetail = await _roomDetailsMastersService.Add_RoomDetails(roomDetailsMaster);
                if (myRoomDetail.Id != null)
                    return Created("Added created Successfully", myRoomDetail);
                return BadRequest(new Error(1, $"Room type {roomDetailsMaster.Id} is Present already"));
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

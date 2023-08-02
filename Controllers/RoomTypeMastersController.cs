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
using MakeYourTrip.Models.DTO;

namespace MakeYourTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeMastersController : ControllerBase
    {
        private readonly IRoomTypeMastersService _roomTypeMastersService;

        public RoomTypeMastersController(IRoomTypeMastersService roomTypeMastersService)
        {
            _roomTypeMastersService = roomTypeMastersService;
        }

        [ProducesResponseType(typeof(RoomTypeMaster), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomTypeMaster>>> GetRoomTypeMasters()
        {
            try
            {
                var myRoomType = await _roomTypeMastersService.View_All_RoomType();
                if (myRoomType.Count > 0)
                    return Ok(myRoomType);
                return BadRequest(new Error(10, "No room types are Existing"));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }



        [ProducesResponseType(typeof(RoomTypeMaster), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public async Task<ActionResult<RoomTypeMaster>> PostRoomTypeMaster(RoomTypeMaster roomTypeMaster)
        {
            try
            {
                var myRoomType = await _roomTypeMastersService.Add_RoomType(roomTypeMaster);
                if (myRoomType.Id != null)
                    return Created("Added created Successfully", myRoomType);
                return BadRequest(new Error(1, $"Room type {roomTypeMaster.Id} is Present already"));
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

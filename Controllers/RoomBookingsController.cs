using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeYourTrip.Models;
using MakeYourTrip.Exceptions;
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Interfaces;

namespace MakeYourTrip.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomBookingsController : ControllerBase
    {
        private readonly IRoomBookingsService _RoomBookingService;


        public RoomBookingsController(IRoomBookingsService RoomBookingService)
        {
            _RoomBookingService = RoomBookingService;
        }

        [ProducesResponseType(typeof(RoomBooking), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public async Task<ActionResult<RoomBooking>> Add_RoomBooking(RoomBooking newHotel)
        {
            
            var myRoomBooking = await _RoomBookingService.Add_RoomBooking(newHotel);
            if (myRoomBooking != null)
                return Created("RoomBooking created Successfully", myRoomBooking);
            return BadRequest(new Error(1, $"RoomBooking {newHotel.Id} is Present already"));
           
        }


        [ProducesResponseType(typeof(RoomBooking), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]

        public async Task<ActionResult<List<RoomBooking>>> Get_all_RoomBooking()
        {
            var myRoomBookings = await _RoomBookingService.View_All_RoomBookings();
            if (myRoomBookings?.Count > 0)
                return Ok(myRoomBookings);
            return BadRequest(new Error(10, "No RoomBookings are Existing"));
        }

    }
}

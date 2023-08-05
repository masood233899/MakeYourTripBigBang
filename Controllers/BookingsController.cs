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
    public class BookingsController : ControllerBase
    {
        private readonly IBookingsService _bookingsService;

        public BookingsController(IBookingsService bookingsService)
        {
            _bookingsService = bookingsService;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            try
            {
                var myBookings = await _bookingsService.View_All_Bookings();
                if (myBookings.Count > 0)
                    return Ok(myBookings);
                return BadRequest(new Error(10, "No Bookings are Existing"));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }

        

        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            try
            {
                var myBooking = await _bookingsService.Add_Booking(booking);
                if (myBooking != null)
                    return Created("Booked Successfully", myBooking);
                return BadRequest(new Error(1, $"Booking {booking.Id} is Present already"));
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

        [ProducesResponseType(typeof(Booking), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]

        public async Task<ActionResult<Booking>> View_Booking(IdDTO idDTO)
        {
            try
            {
                if (idDTO.Idint <= 0)
                    return BadRequest(new Error(4, "Enter Valid Booking ID"));
                var myBooking = await _bookingsService.View_Booking(idDTO);
                if (myBooking != null)
                    return Created("Booking", myBooking);
                return BadRequest(new Error(9, $"There is no Booking present for the id {idDTO.Idint}"));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }
    }
}

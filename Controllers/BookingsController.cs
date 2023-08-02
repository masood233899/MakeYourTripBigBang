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
    [Route("api/[controller]")]
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

        // GET: api/Bookings/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Booking>> GetBooking(IdDTO idDTO)
        //{
        //    try
        //    {
        //        /* if (PlaceMaster.Id <= 0)
        //             throw new InvalidPrimaryID();*/
        //        var myBooking = await _bookingsService.View_Booking(idDTO);
        //        if (myBooking.Id != null)
        //            return Created("Boo created Successfully", myBooking);
        //        return BadRequest(new Error(1, $"PlaceMaster {PlaceMaster.Id} is Present already"));
        //    }
        //    catch (InvalidPrimaryKeyId ip)
        //    {
        //        return BadRequest(new Error(2, ip.Message));
        //    }
        //    catch (InvalidSqlException ise)
        //    {
        //        return BadRequest(new Error(25, ise.Message));
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            try
            {
                var myBooking = await _bookingsService.Add_Booking(booking);
                if (myBooking.Id != null)
                    return Created("Added created Successfully", myBooking);
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
    }
}

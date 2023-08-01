using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeYourTrip.Models;

namespace MakeYourTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomBookingsController : ControllerBase
    {
        private readonly TourPackagesContext _context;

        public RoomBookingsController(TourPackagesContext context)
        {
            _context = context;
        }

        // GET: api/RoomBookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomBooking>>> GetRoomBookings()
        {
          if (_context.RoomBookings == null)
          {
              return NotFound();
          }
            return await _context.RoomBookings.ToListAsync();
        }

        // GET: api/RoomBookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomBooking>> GetRoomBooking(int id)
        {
          if (_context.RoomBookings == null)
          {
              return NotFound();
          }
            var roomBooking = await _context.RoomBookings.FindAsync(id);

            if (roomBooking == null)
            {
                return NotFound();
            }

            return roomBooking;
        }

        // PUT: api/RoomBookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomBooking(int id, RoomBooking roomBooking)
        {
            if (id != roomBooking.Id)
            {
                return BadRequest();
            }

            _context.Entry(roomBooking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomBookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RoomBookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomBooking>> PostRoomBooking(RoomBooking roomBooking)
        {
          if (_context.RoomBookings == null)
          {
              return Problem("Entity set 'TourPackagesContext.RoomBookings'  is null.");
          }
            _context.RoomBookings.Add(roomBooking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoomBooking", new { id = roomBooking.Id }, roomBooking);
        }

        // DELETE: api/RoomBookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomBooking(int id)
        {
            if (_context.RoomBookings == null)
            {
                return NotFound();
            }
            var roomBooking = await _context.RoomBookings.FindAsync(id);
            if (roomBooking == null)
            {
                return NotFound();
            }

            _context.RoomBookings.Remove(roomBooking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomBookingExists(int id)
        {
            return (_context.RoomBookings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

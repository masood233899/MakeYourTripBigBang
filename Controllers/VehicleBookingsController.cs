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
    public class VehicleBookingsController : ControllerBase
    {
        private readonly TourPackagesContext _context;

        public VehicleBookingsController(TourPackagesContext context)
        {
            _context = context;
        }

        // GET: api/VehicleBookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleBooking>>> GetVehicleBookings()
        {
          if (_context.VehicleBookings == null)
          {
              return NotFound();
          }
            return await _context.VehicleBookings.ToListAsync();
        }

        // GET: api/VehicleBookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleBooking>> GetVehicleBooking(int id)
        {
          if (_context.VehicleBookings == null)
          {
              return NotFound();
          }
            var vehicleBooking = await _context.VehicleBookings.FindAsync(id);

            if (vehicleBooking == null)
            {
                return NotFound();
            }

            return vehicleBooking;
        }

        // PUT: api/VehicleBookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleBooking(int id, VehicleBooking vehicleBooking)
        {
            if (id != vehicleBooking.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicleBooking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleBookingExists(id))
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

        // POST: api/VehicleBookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VehicleBooking>> PostVehicleBooking(VehicleBooking vehicleBooking)
        {
          if (_context.VehicleBookings == null)
          {
              return Problem("Entity set 'TourPackagesContext.VehicleBookings'  is null.");
          }
            _context.VehicleBookings.Add(vehicleBooking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleBooking", new { id = vehicleBooking.Id }, vehicleBooking);
        }

        // DELETE: api/VehicleBookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleBooking(int id)
        {
            if (_context.VehicleBookings == null)
            {
                return NotFound();
            }
            var vehicleBooking = await _context.VehicleBookings.FindAsync(id);
            if (vehicleBooking == null)
            {
                return NotFound();
            }

            _context.VehicleBookings.Remove(vehicleBooking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleBookingExists(int id)
        {
            return (_context.VehicleBookings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

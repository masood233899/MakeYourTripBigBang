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
    public class VehicleDetailsMastersController : ControllerBase
    {
        private readonly TourPackagesContext _context;

        public VehicleDetailsMastersController(TourPackagesContext context)
        {
            _context = context;
        }

        // GET: api/VehicleDetailsMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDetailsMaster>>> GetVehicleDetailsMasters()
        {
          if (_context.VehicleDetailsMasters == null)
          {
              return NotFound();
          }
            return await _context.VehicleDetailsMasters.ToListAsync();
        }

        // GET: api/VehicleDetailsMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDetailsMaster>> GetVehicleDetailsMaster(int id)
        {
          if (_context.VehicleDetailsMasters == null)
          {
              return NotFound();
          }
            var vehicleDetailsMaster = await _context.VehicleDetailsMasters.FindAsync(id);

            if (vehicleDetailsMaster == null)
            {
                return NotFound();
            }

            return vehicleDetailsMaster;
        }

        // PUT: api/VehicleDetailsMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleDetailsMaster(int id, VehicleDetailsMaster vehicleDetailsMaster)
        {
            if (id != vehicleDetailsMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicleDetailsMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleDetailsMasterExists(id))
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

        // POST: api/VehicleDetailsMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VehicleDetailsMaster>> PostVehicleDetailsMaster(VehicleDetailsMaster vehicleDetailsMaster)
        {
          if (_context.VehicleDetailsMasters == null)
          {
              return Problem("Entity set 'TourPackagesContext.VehicleDetailsMasters'  is null.");
          }
            _context.VehicleDetailsMasters.Add(vehicleDetailsMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleDetailsMaster", new { id = vehicleDetailsMaster.Id }, vehicleDetailsMaster);
        }

        // DELETE: api/VehicleDetailsMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleDetailsMaster(int id)
        {
            if (_context.VehicleDetailsMasters == null)
            {
                return NotFound();
            }
            var vehicleDetailsMaster = await _context.VehicleDetailsMasters.FindAsync(id);
            if (vehicleDetailsMaster == null)
            {
                return NotFound();
            }

            _context.VehicleDetailsMasters.Remove(vehicleDetailsMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleDetailsMasterExists(int id)
        {
            return (_context.VehicleDetailsMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

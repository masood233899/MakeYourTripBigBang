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
    public class VehicleMastersController : ControllerBase
    {
        private readonly TourPackagesContext _context;

        public VehicleMastersController(TourPackagesContext context)
        {
            _context = context;
        }

        // GET: api/VehicleMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleMaster>>> GetVehicleMasters()
        {
          if (_context.VehicleMasters == null)
          {
              return NotFound();
          }
            return await _context.VehicleMasters.ToListAsync();
        }

        // GET: api/VehicleMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleMaster>> GetVehicleMaster(int id)
        {
          if (_context.VehicleMasters == null)
          {
              return NotFound();
          }
            var vehicleMaster = await _context.VehicleMasters.FindAsync(id);

            if (vehicleMaster == null)
            {
                return NotFound();
            }

            return vehicleMaster;
        }

        // PUT: api/VehicleMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleMaster(int id, VehicleMaster vehicleMaster)
        {
            if (id != vehicleMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicleMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleMasterExists(id))
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

        // POST: api/VehicleMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VehicleMaster>> PostVehicleMaster(VehicleMaster vehicleMaster)
        {
          if (_context.VehicleMasters == null)
          {
              return Problem("Entity set 'TourPackagesContext.VehicleMasters'  is null.");
          }
            _context.VehicleMasters.Add(vehicleMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleMaster", new { id = vehicleMaster.Id }, vehicleMaster);
        }

        // DELETE: api/VehicleMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleMaster(int id)
        {
            if (_context.VehicleMasters == null)
            {
                return NotFound();
            }
            var vehicleMaster = await _context.VehicleMasters.FindAsync(id);
            if (vehicleMaster == null)
            {
                return NotFound();
            }

            _context.VehicleMasters.Remove(vehicleMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleMasterExists(int id)
        {
            return (_context.VehicleMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

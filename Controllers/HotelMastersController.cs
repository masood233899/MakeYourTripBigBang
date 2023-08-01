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
    public class HotelMastersController : ControllerBase
    {
        private readonly TourPackagesContext _context;

        public HotelMastersController(TourPackagesContext context)
        {
            _context = context;
        }

        // GET: api/HotelMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelMaster>>> GetHotelMasters()
        {
          if (_context.HotelMasters == null)
          {
              return NotFound();
          }
            return await _context.HotelMasters.ToListAsync();
        }

        // GET: api/HotelMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelMaster>> GetHotelMaster(int id)
        {
          if (_context.HotelMasters == null)
          {
              return NotFound();
          }
            var hotelMaster = await _context.HotelMasters.FindAsync(id);

            if (hotelMaster == null)
            {
                return NotFound();
            }

            return hotelMaster;
        }

        // PUT: api/HotelMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelMaster(int id, HotelMaster hotelMaster)
        {
            if (id != hotelMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotelMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelMasterExists(id))
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

        // POST: api/HotelMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelMaster>> PostHotelMaster(HotelMaster hotelMaster)
        {
          if (_context.HotelMasters == null)
          {
              return Problem("Entity set 'TourPackagesContext.HotelMasters'  is null.");
          }
            _context.HotelMasters.Add(hotelMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotelMaster", new { id = hotelMaster.Id }, hotelMaster);
        }

        // DELETE: api/HotelMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelMaster(int id)
        {
            if (_context.HotelMasters == null)
            {
                return NotFound();
            }
            var hotelMaster = await _context.HotelMasters.FindAsync(id);
            if (hotelMaster == null)
            {
                return NotFound();
            }

            _context.HotelMasters.Remove(hotelMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelMasterExists(int id)
        {
            return (_context.HotelMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

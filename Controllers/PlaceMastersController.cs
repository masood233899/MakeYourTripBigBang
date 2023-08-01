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
    public class PlaceMastersController : ControllerBase
    {
        private readonly TourPackagesContext _context;

        public PlaceMastersController(TourPackagesContext context)
        {
            _context = context;
        }

        // GET: api/PlaceMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceMaster>>> GetPlaceMasters()
        {
          if (_context.PlaceMasters == null)
          {
              return NotFound();
          }
            return await _context.PlaceMasters.ToListAsync();
        }

        // GET: api/PlaceMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceMaster>> GetPlaceMaster(int id)
        {
          if (_context.PlaceMasters == null)
          {
              return NotFound();
          }
            var placeMaster = await _context.PlaceMasters.FindAsync(id);

            if (placeMaster == null)
            {
                return NotFound();
            }

            return placeMaster;
        }

        // PUT: api/PlaceMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaceMaster(int id, PlaceMaster placeMaster)
        {
            if (id != placeMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(placeMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceMasterExists(id))
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

        // POST: api/PlaceMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlaceMaster>> PostPlaceMaster(PlaceMaster placeMaster)
        {
          if (_context.PlaceMasters == null)
          {
              return Problem("Entity set 'TourPackagesContext.PlaceMasters'  is null.");
          }
            _context.PlaceMasters.Add(placeMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlaceMaster", new { id = placeMaster.Id }, placeMaster);
        }

        // DELETE: api/PlaceMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaceMaster(int id)
        {
            if (_context.PlaceMasters == null)
            {
                return NotFound();
            }
            var placeMaster = await _context.PlaceMasters.FindAsync(id);
            if (placeMaster == null)
            {
                return NotFound();
            }

            _context.PlaceMasters.Remove(placeMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlaceMasterExists(int id)
        {
            return (_context.PlaceMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

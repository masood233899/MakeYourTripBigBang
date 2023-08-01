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
    public class RoomTypeMastersController : ControllerBase
    {
        private readonly TourPackagesContext _context;

        public RoomTypeMastersController(TourPackagesContext context)
        {
            _context = context;
        }

        // GET: api/RoomTypeMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomTypeMaster>>> GetRoomTypeMasters()
        {
          if (_context.RoomTypeMasters == null)
          {
              return NotFound();
          }
            return await _context.RoomTypeMasters.ToListAsync();
        }

        // GET: api/RoomTypeMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomTypeMaster>> GetRoomTypeMaster(int id)
        {
          if (_context.RoomTypeMasters == null)
          {
              return NotFound();
          }
            var roomTypeMaster = await _context.RoomTypeMasters.FindAsync(id);

            if (roomTypeMaster == null)
            {
                return NotFound();
            }

            return roomTypeMaster;
        }

        // PUT: api/RoomTypeMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomTypeMaster(int id, RoomTypeMaster roomTypeMaster)
        {
            if (id != roomTypeMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(roomTypeMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomTypeMasterExists(id))
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

        // POST: api/RoomTypeMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomTypeMaster>> PostRoomTypeMaster(RoomTypeMaster roomTypeMaster)
        {
          if (_context.RoomTypeMasters == null)
          {
              return Problem("Entity set 'TourPackagesContext.RoomTypeMasters'  is null.");
          }
            _context.RoomTypeMasters.Add(roomTypeMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoomTypeMaster", new { id = roomTypeMaster.Id }, roomTypeMaster);
        }

        // DELETE: api/RoomTypeMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomTypeMaster(int id)
        {
            if (_context.RoomTypeMasters == null)
            {
                return NotFound();
            }
            var roomTypeMaster = await _context.RoomTypeMasters.FindAsync(id);
            if (roomTypeMaster == null)
            {
                return NotFound();
            }

            _context.RoomTypeMasters.Remove(roomTypeMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomTypeMasterExists(int id)
        {
            return (_context.RoomTypeMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

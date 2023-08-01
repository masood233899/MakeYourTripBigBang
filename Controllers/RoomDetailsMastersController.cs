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
    public class RoomDetailsMastersController : ControllerBase
    {
        private readonly TourPackagesContext _context;

        public RoomDetailsMastersController(TourPackagesContext context)
        {
            _context = context;
        }

        // GET: api/RoomDetailsMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDetailsMaster>>> GetRoomDetailsMasters()
        {
          if (_context.RoomDetailsMasters == null)
          {
              return NotFound();
          }
            return await _context.RoomDetailsMasters.ToListAsync();
        }

        // GET: api/RoomDetailsMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDetailsMaster>> GetRoomDetailsMaster(int id)
        {
          if (_context.RoomDetailsMasters == null)
          {
              return NotFound();
          }
            var roomDetailsMaster = await _context.RoomDetailsMasters.FindAsync(id);

            if (roomDetailsMaster == null)
            {
                return NotFound();
            }

            return roomDetailsMaster;
        }

        // PUT: api/RoomDetailsMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomDetailsMaster(int id, RoomDetailsMaster roomDetailsMaster)
        {
            if (id != roomDetailsMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(roomDetailsMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomDetailsMasterExists(id))
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

        // POST: api/RoomDetailsMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomDetailsMaster>> PostRoomDetailsMaster(RoomDetailsMaster roomDetailsMaster)
        {
          if (_context.RoomDetailsMasters == null)
          {
              return Problem("Entity set 'TourPackagesContext.RoomDetailsMasters'  is null.");
          }
            _context.RoomDetailsMasters.Add(roomDetailsMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoomDetailsMaster", new { id = roomDetailsMaster.Id }, roomDetailsMaster);
        }

        // DELETE: api/RoomDetailsMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomDetailsMaster(int id)
        {
            if (_context.RoomDetailsMasters == null)
            {
                return NotFound();
            }
            var roomDetailsMaster = await _context.RoomDetailsMasters.FindAsync(id);
            if (roomDetailsMaster == null)
            {
                return NotFound();
            }

            _context.RoomDetailsMasters.Remove(roomDetailsMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomDetailsMasterExists(int id)
        {
            return (_context.RoomDetailsMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

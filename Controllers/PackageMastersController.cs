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
    public class PackageMastersController : ControllerBase
    {
        private readonly TourPackagesContext _context;

        public PackageMastersController(TourPackagesContext context)
        {
            _context = context;
        }

        // GET: api/PackageMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageMaster>>> GetPackageMasters()
        {
          if (_context.PackageMasters == null)
          {
              return NotFound();
          }
            return await _context.PackageMasters.ToListAsync();
        }

        // GET: api/PackageMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PackageMaster>> GetPackageMaster(int id)
        {
          if (_context.PackageMasters == null)
          {
              return NotFound();
          }
            var packageMaster = await _context.PackageMasters.FindAsync(id);

            if (packageMaster == null)
            {
                return NotFound();
            }

            return packageMaster;
        }

        // PUT: api/PackageMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackageMaster(int id, PackageMaster packageMaster)
        {
            if (id != packageMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(packageMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageMasterExists(id))
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

        // POST: api/PackageMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PackageMaster>> PostPackageMaster(PackageMaster packageMaster)
        {
          if (_context.PackageMasters == null)
          {
              return Problem("Entity set 'TourPackagesContext.PackageMasters'  is null.");
          }
            _context.PackageMasters.Add(packageMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPackageMaster", new { id = packageMaster.Id }, packageMaster);
        }

        // DELETE: api/PackageMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackageMaster(int id)
        {
            if (_context.PackageMasters == null)
            {
                return NotFound();
            }
            var packageMaster = await _context.PackageMasters.FindAsync(id);
            if (packageMaster == null)
            {
                return NotFound();
            }

            _context.PackageMasters.Remove(packageMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackageMasterExists(int id)
        {
            return (_context.PackageMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

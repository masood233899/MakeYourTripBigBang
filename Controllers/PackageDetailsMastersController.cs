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
    public class PackageDetailsMastersController : ControllerBase
    {
        private readonly TourPackagesContext _context;

        public PackageDetailsMastersController(TourPackagesContext context)
        {
            _context = context;
        }

        // GET: api/PackageDetailsMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageDetailsMaster>>> GetPackageDetailsMasters()
        {
          if (_context.PackageDetailsMasters == null)
          {
              return NotFound();
          }
            return await _context.PackageDetailsMasters.ToListAsync();
        }

        // GET: api/PackageDetailsMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PackageDetailsMaster>> GetPackageDetailsMaster(int id)
        {
          if (_context.PackageDetailsMasters == null)
          {
              return NotFound();
          }
            var packageDetailsMaster = await _context.PackageDetailsMasters.FindAsync(id);

            if (packageDetailsMaster == null)
            {
                return NotFound();
            }

            return packageDetailsMaster;
        }

        // PUT: api/PackageDetailsMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackageDetailsMaster(int id, PackageDetailsMaster packageDetailsMaster)
        {
            if (id != packageDetailsMaster.Id)
            {
                return BadRequest();
            }

            _context.Entry(packageDetailsMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageDetailsMasterExists(id))
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

        // POST: api/PackageDetailsMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PackageDetailsMaster>> PostPackageDetailsMaster(PackageDetailsMaster packageDetailsMaster)
        {
          if (_context.PackageDetailsMasters == null)
          {
              return Problem("Entity set 'TourPackagesContext.PackageDetailsMasters'  is null.");
          }
            _context.PackageDetailsMasters.Add(packageDetailsMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPackageDetailsMaster", new { id = packageDetailsMaster.Id }, packageDetailsMaster);
        }

        // DELETE: api/PackageDetailsMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackageDetailsMaster(int id)
        {
            if (_context.PackageDetailsMasters == null)
            {
                return NotFound();
            }
            var packageDetailsMaster = await _context.PackageDetailsMasters.FindAsync(id);
            if (packageDetailsMaster == null)
            {
                return NotFound();
            }

            _context.PackageDetailsMasters.Remove(packageDetailsMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackageDetailsMasterExists(int id)
        {
            return (_context.PackageDetailsMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

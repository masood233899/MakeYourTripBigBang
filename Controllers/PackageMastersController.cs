using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeYourTrip.Models;
using MakeYourTrip.Exceptions;
using MakeYourTrip.Interfaces;

namespace MakeYourTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageMastersController : ControllerBase
    {
        private readonly IPackageMastersService _packageMastersService;

        public PackageMastersController(IPackageMastersService packageMastersService)
        {
            _packageMastersService = packageMastersService;
        }

        // GET: api/PackageMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageMaster>>> GetPackageMasters()
        {
            try
            {
                var mypackages = await _packageMastersService.View_All_PackageMaster();
                if (mypackages.Count > 0)
                    return Ok(mypackages);
                return BadRequest(new Error(10, "No packages are Existing"));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }


        // POST: api/PackageMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PackageMaster>> PostPackageMaster(PackageMaster packageMaster)
        {
            try
            {
                var mypackage = await _packageMastersService.Add_PackageMaster(packageMaster);
                if (mypackage.Id != null)
                    return Created("Added created Successfully", mypackage);
                return BadRequest(new Error(1, $"Package {packageMaster.Id} is Present already"));
            }
            catch (InvalidPrimaryKeyId ip)
            {
                return BadRequest(new Error(2, ip.Message));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }
    }
}

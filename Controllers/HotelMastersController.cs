﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeYourTrip.Models;
using MakeYourTrip.Exceptions;
using MakeYourTrip.Interfaces;
using MakeYourTrip.Models.DTO;

namespace MakeYourTrip.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HotelMastersController : ControllerBase
    {
        private readonly IHotelMastersService _hotelMasterService;

        public HotelMastersController(IHotelMastersService hotelMasterService)
        {
            _hotelMasterService = hotelMasterService;
        }

        // GET: api/HotelMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelMaster>>> GetHotelMasters()
        {
            try
            {
                var myhotel = await _hotelMasterService.View_All_HotelMaster();
                if (myhotel?.Count > 0)
                    return Ok(myhotel);
                return BadRequest(new Error(10, "No hotels are Existing"));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }


        [ProducesResponseType(typeof(HotelMaster), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public async Task<ActionResult<HotelMaster>> Add_HotelMaster(HotelMaster hotelMaster)
        {
            try
            {
                var myhotel = await _hotelMasterService.Add_HotelMaster(hotelMaster);
                if (myhotel.Id != null)
                    return Created("Hotel Added Successfully", myhotel);
                return BadRequest(new Error(1, $"hotel {myhotel.Id} is Present already"));
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
        [ProducesResponseType(typeof(HotelMaster), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]

        public async Task<ActionResult<HotelMaster>> PostHotelMaster([FromForm] HotelFormModule hotelFormModule)
        {
            try
            {
                var createdHotel = await _hotelMasterService.PostImage(hotelFormModule);
                return Ok(createdHotel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
}

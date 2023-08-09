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
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Services;

namespace MakeYourTrip.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostGalleriesController : ControllerBase
    {
        private readonly IPostGalleryService _PostGalleryService;


        public PostGalleriesController(IPostGalleryService PostGalleryService)
        {
            _PostGalleryService = PostGalleryService;
        }

        [ProducesResponseType(typeof(PostGallery), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public async Task<ActionResult<PostGallery>> Add_PostGallery(PostGallery newplace)
        {
            /* try
             {*/
            /* if (additionalCategoryMaster.Id <=0)
                 throw new InvalidPrimaryID();*/
            var myPostGallery = await _PostGalleryService.Add_PostGallery(newplace);
            if (myPostGallery != null)
                return Created("PostGallery created Successfully", myPostGallery);
            return BadRequest(new Error(1, $"PostGallery {newplace.Id} is Present already"));
            /*}
            catch (InvalidPrimaryID ip)
            {
                return BadRequest(new Error(2, ip.Message));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }*/
        }

        [ProducesResponseType(typeof(PostGallery), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]

        public async Task<ActionResult<List<PostGallery>>> Get_all_PostGallery()
        {
            var myPostGallerys = await _PostGalleryService.Get_all_PostGallery();
            if (myPostGallerys?.Count > 0)
                return Ok(myPostGallerys);
            return BadRequest(new Error(10, "No PostGallery are Existing"));
        }

        [ProducesResponseType(typeof(PostGallery), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]

        public async Task<ActionResult<PostGallery>> View_PostGallery(IdDTO idDTO)
        {
            try
            {
                if (idDTO.Idint <= 0)
                    return BadRequest(new Error(4, "Enter Valid PostGallery ID"));
                var myPostGallery = await _PostGalleryService.View_PostGallery(idDTO);
                if (myPostGallery != null)
                    return Created("PostGallery", myPostGallery);
                return BadRequest(new Error(9, $"There is no PostGallery present for the id {idDTO.Idint}"));
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(25, ise.Message));
            }
        }

        [ProducesResponseType(typeof(PostGallery), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]

        public async Task<ActionResult<PostGallery>> PostPostGalleryImage([FromForm] PostGalleryFormModule postGalleryFormModule)
        {
            try
            {
                var createdHotel = await _PostGalleryService.PostImage(postGalleryFormModule);
                return Ok(createdHotel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

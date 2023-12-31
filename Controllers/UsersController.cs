﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeYourTrip.Models;
using MakeYourTrip.Exceptions;
using MakeYourTrip.Models.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MakeYourTrip.Interfaces;
using Error = MakeYourTrip.Models.Error;
using Microsoft.AspNetCore.Authorization;

namespace MakeYourTrip.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Register(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                UserDTO user = await _userService.Register(userRegisterDTO);
                if (user == null)
                    return BadRequest(new Error(2, "Registration Not Successful"));
                return Created("User Registered", user);
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(3, ise.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error(4, ex.Message));
            }
        }


        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public async Task<ActionResult<UserDTO>> LogIN(UserDTO userDTO)
        {
            try
            {
                UserDTO user = await _userService.Login(userDTO);
                if (user == null)
                    return BadRequest(new Error(1, "Invalid Username or Password"));
                return Ok(user);
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(3, ise.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error(4, ex.Message));
            }
        }

        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPut]
        public async Task<ActionResult<UserDTO>> Update(UserRegisterDTO user)
        {
            try
            {
                var myUser = await _userService.Update(user);
                if (myUser == null)
                    return NotFound(new Error(3, "Unable to Update"));
                return Created("User Updated Successfully", myUser);
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(3, ise.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error(4, ex.Message));
            }
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPut]
        public async Task<ActionResult<string>> Update_Password(UserDTO user)
        {
            try
            {
                bool myUser = await _userService.UpdatePassword(user);
                if (myUser)
                    return NotFound(new Error(3, "Unable to Update Password"));
                return Ok("Password Updated Successfully");
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(3, ise.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error(4, ex.Message));
            }
        }
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPut]

        public async Task<ActionResult<User>?> ApproveAgent(User agent)
        {

            var newagent = await _userService.ApproveAgent(agent);
            if (newagent != null)
            {
                return Ok(newagent);
            }
            return null;

        }
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpGet]

        public async Task<ActionResult<List<User>?>> GetUnApprovedAgent()
        {
            var newagent = await _userService.GetUnApprovedAgent();
            if (newagent != null)
            {
                return Ok(newagent);
            }
            return BadRequest(new Error(4, "no unapproved agent exist"));
        }

        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpDelete]

        public async Task<ActionResult<User>> DeleteAgent(UserDTO user)
        {
            var deletedagent = await _userService.DeleteAgent(user);
            if (deletedagent != null)
            {
                return Ok(deletedagent);
            }
            return BadRequest(new Error(4, "no  agent exist"));
        }
    }
}

﻿using BussinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollabsController : ControllerBase
    {
        private readonly ICollaboratorBL icollabBL;
        public CollabsController(ICollaboratorBL icollabBL)
        {
            this.icollabBL = icollabBL;
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateCollab(long NoteID, string Email)
        {
            try
            {
                var result = icollabBL.CreateCollab(NoteID, Email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Collaborator Created successfully", data = result });
                }
                else
                {
                    return BadRequest(new {success = false, message = "Cannot create collaborator."});
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("Get")]
        public IActionResult GetCollab()
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = icollabBL.GetCollab(userID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Got all collaborator notes.", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot get collaborator." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}

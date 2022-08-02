using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteBL inoteBL;
        public NotesController(INoteBL inoteBL)
        {
            this.inoteBL = inoteBL;
        }

        [Authorize]
        [HttpPost]
        [Route("CreateNote")]
        public IActionResult CreateNote(NoteModel noteModel)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBL.CreateNote(noteModel, userID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Created SuccessFully with Title " + noteModel.Title, data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot create note." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("ReadNote")]
        public IActionResult ReadNotes()
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBL.ReadNotes(userID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Got Notes from databse.", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot get notes." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}

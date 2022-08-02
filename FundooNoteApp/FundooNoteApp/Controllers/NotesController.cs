using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using System;
using System.Linq;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INoteBL inoteBL;
        public NotesController(INoteBL inoteBL)
        {
            this.inoteBL = inoteBL;
        }

        [HttpPost]
        [Route("Create")]
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
        [HttpGet]
        [Route("Read")]
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
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteNotes(long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBL.DeleteNotes(userID, NoteID);
                if (result != false)
                {
                    return Ok(new { success = true, message = "Note Deleted." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot delete note." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateNote(NoteModel noteModel, long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBL.UpdateNote(noteModel, NoteID, userID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Updated Successfully.", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot update note." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("Pin")]
        public IActionResult pinToDashboard(long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = inoteBL.PinToDashboard(NoteID, userID);
                if (result == true)
                {
                    return Ok(new { success = true, message = "Note Pinned Successfully"});
                }
                else if (result == false)
                {
                    return Ok(new { success = true, message = "Note Unpinned successfully." });
                }
                return BadRequest(new { success = false, message = "Cannot perform operation." });
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}

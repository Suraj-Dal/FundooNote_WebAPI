using BussinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollabsController : ControllerBase
    {
        private readonly ICollaboratorBL icollabBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly fundooContext fundooContext;
        public CollabsController(ICollaboratorBL icollabBL, IMemoryCache memoryCache, IDistributedCache distributedCache, fundooContext fundooContext)
        {
            this.icollabBL = icollabBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
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
        [HttpDelete]
        [Route("Delete")]
        public IActionResult RemoveCollab(long CollabID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = icollabBL.RemoveCollab(CollabID, userID);
                if (result)
                {
                    return Ok(new { success = true, message = "Removed Collaborator.", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot remove collaborator." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpGet("Redis")]
        public async Task<IActionResult> GetAllCollabsUsingRedisCache()
        {
            var cacheKey = "collabsList";
            string serializedCollabsList;
            var collabsList = new List<CollaboratorEntity>();
            var redisCollabsList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabsList != null)
            {
                serializedCollabsList = Encoding.UTF8.GetString(redisCollabsList);
                collabsList = JsonConvert.DeserializeObject<List<CollaboratorEntity>>(serializedCollabsList);
            }
            else
            {
                collabsList = fundooContext.collaboratorEntities.ToList();
                serializedCollabsList = JsonConvert.SerializeObject(collabsList);
                redisCollabsList = Encoding.UTF8.GetBytes(serializedCollabsList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabsList, options);
            }
            return Ok(collabsList);
        }

    }
}

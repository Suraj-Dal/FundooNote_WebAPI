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
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL iLabelBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly fundooContext fundooContext;
        public LabelController(ILabelBL iLabelBL, IMemoryCache memoryCache, IDistributedCache distributedCache, fundooContext fundooContext)
        {
            this.iLabelBL = iLabelBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateLabel(string Name, long noteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iLabelBL.CreateLabel(Name, noteID, userID);
                if (result)
                {
                    return Ok(new { success = true, message = "Label Created" });
                }
                else
                {
                    return BadRequest(new {success = false, message = "Cannot create Label" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("Get")]
        public IActionResult GetLabel(long LabelID)
        {
            try
            {
                var result = iLabelBL.GetLabel(LabelID);
                if (result != null)
                {
                    return Ok(new {success = true, message = "Got all Labels", data = result});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot get labels." });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete]
        [Route("Delete")]
        public IActionResult RemoveLabel(long LabelID)
        {
            try
            {
                var result = iLabelBL.RemoveLabel(LabelID);
                if (result)
                {
                    return Ok(new { success = true, message = "Ladel removed" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "cannot remove label" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateLabel(string name, long noteID)
        {
            try
            {
                var result = iLabelBL.UpdateLabel(name, noteID);
                if (result)
                {
                    return Ok(new { success = true, message = "Label updated." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot update label" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("Redis")]
        public async Task<IActionResult> GetAllLabelsUsingRedisCache()
        {
            var cacheKey = "labelList";
            string serializedLabelList;
            var labelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                labelList = JsonConvert.DeserializeObject < List <LabelEntity>>(serializedLabelList);
            }
            else
            {
                labelList = fundooContext.labelEntities.ToList();
                serializedLabelList = JsonConvert.SerializeObject(labelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(labelList);
        }
    }
}

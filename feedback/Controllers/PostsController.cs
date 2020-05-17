using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using feedback.Management;
using feedback.Models;
using feedback.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace feedback.Controllers
{
    [Route("api/[controller]"), Produces("application/json"), EnableCors("AppPolicy")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        #region Variable Declaration & Initialization
        private PostMgt _manager = null;
        #endregion

        #region Constructor
        public PostsController()
        {
            _manager = new PostMgt();
        }
        #endregion
        
        // POST: api/users/saveupdate
        [HttpPost("[action]")]
        public async Task<object> createPost([FromBody]object[] data)
        {
            object result = null; object resdata = null;
            try
            {
                var _User = JsonConvert.DeserializeObject<Post>(data[0].ToString());
                if (_User != null)
                {
                    resdata = await _manager.CreatePost(_User);
                }
            }
            catch (Exception) { }

            return result = new
            {
                resdata
            };
        }
        [HttpGet("[action]")]
        public async Task<object> getpostdata([FromQuery] string param )//string pageNumber,string pageSize,string search)//[FromQuery] string param
        {
            object result = null; object resdata = null;
            try
            {
                dynamic data = JsonConvert.DeserializeObject(param);
                vmCommonParam cmnParam = JsonConvert.DeserializeObject<vmCommonParam>(data[0].ToString());
                
                resdata = await _manager.GetPostData(cmnParam);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return result = new
            {
                resdata
            };
        }

    }
}
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
    public class CommentController : ControllerBase
    {
        #region Variable Declaration & Initialization
        private CommentMgt _manager = null;
        #endregion

        #region Constructor
        public CommentController()
        {
            _manager = new CommentMgt();
        }
        #endregion
        [HttpPost("[action]")]
        public async Task<object> createComment([FromBody]object[] data)
        {
            object result = null; object resdata = null;
            try
            {
                var _User = JsonConvert.DeserializeObject<Comment>(data[0].ToString());
                if (_User != null)
                {
                    resdata = await _manager.createComment(_User);
                }
            }
            catch (Exception) { }

            return result = new
            {
                resdata
            };
        }
        // POST: api/comments/setOpinion
        [HttpPost("[action]")]
        public async Task<object> setOpinion([FromBody]object[] data)
        {
            object result = null; object resdata = null;
            try
            {
                vmCommonParam _param = JsonConvert.DeserializeObject<vmCommonParam>(data[0].ToString());
                if (_param != null)
                {
                    resdata = await _manager.setOpinion(_param);
                }
            }
            catch (Exception) { }

            return result = new
            {
                resdata
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using feedback.Management;
using feedback.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace feedback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Variable Declaration & Initialization
        private UserMgt _manager = null;
        #endregion

        #region Constructor
        public UsersController()
        {
            _manager = new UserMgt();
        }
        #endregion
        // POST: api/users/saveupdate
        [HttpPost("[action]")]
        public async Task<object> userCreate([FromBody]object[] data)
        {
            object result = null; object resdata = null;
            try
            {
                var _User = JsonConvert.DeserializeObject<User>(data[0].ToString());
                if (_User != null)
                {
                    resdata = await _manager.userCreate(_User);
                }
            }
            catch (Exception) { }

            return result = new
            {
                resdata
            };
        }
        // POST: api/users/saveupdate
        [HttpPost("[action]")]
        public async Task<object> userAuthentication([FromBody]object[] data)
        {
            object result = null; object resdata = null;
            try
            {
                var _User = JsonConvert.DeserializeObject<User>(data[0].ToString());
                if (_User != null)
                {
                    resdata = await _manager.userAuthentication(_User);
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
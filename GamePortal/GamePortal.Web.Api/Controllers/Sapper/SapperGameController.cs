using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GamePortal.Web.Api.Controllers.Sapper
{
    [RoutePrefix("api/SapperGame")]
    public class SapperGameController : ApiController
    {

        /// <summary>
        /// Get user info by id
        /// </summary>
        /// <returns>User info</returns>
        [HttpGet, Route("users/info/{id}")]
        public IHttpActionResult UserInfoById(int id)
        {
            return Ok("User info: id = " + id);
        }

        /// <summary>
        /// Add user
        /// </summary>
        /// <returns>User id</returns>
        [HttpPost, Route("users/add")]
        public IHttpActionResult UserAdd(int id)
        {
            return Ok("User add: id = " + id);
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <returns>User id</returns>
        [HttpPut, Route("users/update/{id}")]
        public IHttpActionResult UserUpdate(int id)
        {
            return Ok("User update: id = " + id);
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <returns>Ok</returns>
        [HttpDelete, Route("users/delete/{id}")]
        public IHttpActionResult UserDelete(int id)
        {
            return Ok("User delete: id = " + id);
        }
    }
}
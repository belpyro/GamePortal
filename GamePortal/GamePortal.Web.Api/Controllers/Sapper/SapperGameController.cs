using System;
using Vitaly.Sapper.Logic.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vitaly.Sapper.Logic.Models;

namespace GamePortal.Web.Api.Controllers.Sapper
{
    [RoutePrefix("api/SapperGame")]
    public class SapperGameController : ApiController
    {
        private readonly ISapperService _sapperService;

        public SapperGameController(ISapperService sapperService)
        {
            this._sapperService = sapperService;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>All users info</returns>
        [HttpGet, Route("users/all")]
        public IHttpActionResult GetAll()
        {
            return Ok(_sapperService.GetAll());
        }

        /// <summary>
        /// Get user info by id
        /// </summary>
        /// <returns>User info</returns>
        [HttpGet, Route("users/info/{id}")]
        public IHttpActionResult UserInfoById(int id)
        {
            var user = _sapperService.UserInfoById(id);
            return user == null ? (IHttpActionResult)NotFound() : Ok(user);
        }

        /// <summary>
        /// Add user
        /// </summary>
        /// <returns>User id</returns>
        [HttpPost, Route("users/add")]
        public IHttpActionResult UserAdd([FromBody] UserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = _sapperService.UserAdd(model);
                return Created($"/users/{result.Id}", result.Id);
            }
            catch (Exception e)
            {
                return (IHttpActionResult)BadRequest();
            }

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
            _sapperService.UserDelete(id);
            return Ok("User deleted: id = " + id);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
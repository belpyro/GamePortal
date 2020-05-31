using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Kbalan.TouchType.Logic.Dto;
using Kbalan.TouchType.Logic.Services;

namespace GamePortal.Web.Api.Controllers.TouchType
{
    [RoutePrefix("api/users")]
    public class TTGUsersController : ApiController
    {
        private readonly IUserService _userService;
        public TTGUsersController(IUserService userService)
        {
            this._userService = userService;
        }

        //Get All RegisterUsers
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        //Get Full User Info by Id
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById([FromUri]int Id)
        {
            return _userService.GetById(Id) == null ? (IHttpActionResult)NotFound() : Ok(_userService.GetById(Id));
        }

        //Add new user
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody]UserSettingDto model)
        {
            return _userService.Add(model) == null ? (IHttpActionResult)Conflict() : Created($"/registerusers/{model.Id}", model);
        }

        //Update User by Id
        [HttpPut]
        [Route("")]
        public IHttpActionResult Update([FromBody]UserDto model)
        {
            _userService.Update(model);
            return StatusCode(HttpStatusCode.NoContent);
        }

        //Delete User by Id
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            _userService.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
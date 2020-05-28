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
    [RoutePrefix("api/registerusers")]
    public class RegisterUsersController : ApiController
    {
        private readonly IUserService _userService;
        public RegisterUsersController(IUserService userService)
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

        //Get User by Id
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById([FromUri]int Id)
        {
            return _userService.GetById(Id) == null ? (IHttpActionResult)NotFound() : Ok(_userService.GetById(Id));
        }

        //Get User by NickName
        [HttpGet]
        [Route("searchbynick")]
        public IHttpActionResult GetByName([FromUri]string nickname)
        {
            return _userService.GetByName(nickname) == null ? (IHttpActionResult)NotFound() : Ok(_userService.GetByName(nickname));
        }

        //Add new user
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody]RegisterUserDto model)
        {
            return _userService.Add(model) == null ? (IHttpActionResult)Conflict() : Created($"/registerusers/{model.Id}", model);
        }

        //Update User by Id
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update(int id, [FromBody]RegisterUserDto model)
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
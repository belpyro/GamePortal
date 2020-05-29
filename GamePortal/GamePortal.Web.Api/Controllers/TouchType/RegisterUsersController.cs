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

        //Get All RegisterUsers
        [HttpGet]
        [Route("full")]
        public IHttpActionResult GetAllFull()
        {
            return Ok(_userService.GetAllFull());
        }

        //Get All RegisterUsers with settings
        [HttpGet]
        [Route("settings")]
        public IHttpActionResult GetAllWithSettings()
        {
            return Ok(_userService.GetAllWithSettings());
        }

        //Get All RegisterUsers with statistic
        [HttpGet]
        [Route("statistic")]
        public IHttpActionResult GetAllWithStatistic()
        {
            return Ok(_userService.GetAllWithStatistic());
        }

        //Get Full User Info by Id
        [HttpGet]
        [Route("{id}/fullbyid")]
        public IHttpActionResult GetFullById([FromUri]int Id)
        {
            return _userService.GetFullById(Id) == null ? (IHttpActionResult)NotFound() : Ok(_userService.GetFullById(Id));
        }

        //Get User Statistic by Id
        [HttpGet]
        [Route("{id}/statisticbyid")]
        public IHttpActionResult GetStatById([FromUri]int Id)
        {
            return _userService.GetStatById(Id) == null ? (IHttpActionResult)NotFound() : Ok(_userService.GetStatById(Id));
        }

        //Get User Setting by Id
        [HttpGet]
        [Route("{id}/settingbyid")]
        public IHttpActionResult GetSetById([FromUri]int Id)
        {
            return _userService.GetSetById(Id) == null ? (IHttpActionResult)NotFound() : Ok(_userService.GetSetById(Id));
        }

        //Get Full User Info by Nick
        [HttpGet]
        [Route("{nick}/fullbynick")]
        public IHttpActionResult GetFullByNick([FromUri]string nick)
        {
            return _userService.GetFullByNick(nick) == null ? (IHttpActionResult)NotFound() : Ok(_userService.GetFullByNick(nick));
        }

        //Get User Statistic by Nick
        [HttpGet]
        [Route("{nick}/statisticbynick")]
        public IHttpActionResult GetStatByNick([FromUri]string nick)
        {
            return _userService.GetStatByNick(nick) == null ? (IHttpActionResult)NotFound() : Ok(_userService.GetStatByNick(nick));
        }

        //Get User Setting by Nick
        [HttpGet]
        [Route("{nick}/settingbynick")]
        public IHttpActionResult GetSetByNick([FromUri]string nick)
        {
            return _userService.GetSetByNick(nick) == null ? (IHttpActionResult)NotFound() : Ok(_userService.GetSetByNick(nick));
        }


        //Add new user
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody]UserDto model)
        {
            return _userService.Add(model) == null ? (IHttpActionResult)Conflict() : Created($"/registerusers/{model.Id}", model);
        }

        //Update User by Id
        [HttpPut]
        [Route("{id}")]
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using FluentValidation;
using FluentValidation.WebApi;
using JetBrains.Annotations;
using Kbalan.TouchType.Logic.Dto;
using Kbalan.TouchType.Logic.Services;
using Kbalan.TouchType.Logic.Validators;

namespace GamePortal.Web.Api.Controllers.TouchType
{
    /// <summary>
    /// Controller for User
    /// </summary>
    [RoutePrefix("api/users")]
    public class TTGUsersController : ApiController
    {
        private readonly IUserService _userService;


        public TTGUsersController([NotNull]IUserService userService)
        {
            this._userService = userService;
        }

        //Get All RegisterUsers
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var result = _userService.GetAll();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)BadRequest(result.Error);
        }

        //Get Full User Info by Id
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById([FromUri]int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than 0");
            }

            var result = _userService.GetById(id);
            if (result.IsFailure)
                return (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
            return result.Value.HasNoValue ? (IHttpActionResult)NotFound() : Ok(result.Value.Value);

        }

        //Add new user
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody] UserSettingDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _userService.Add(model);
            return result.IsSuccess ? Created($"/textsets/{result.Value.Id}", result.Value) : (IHttpActionResult)BadRequest(result.Error);
        }

        //Update User by Id
        [HttpPut]
        [Route("")]
        public IHttpActionResult Update([FromBody]UserDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _userService.Update(model);
            return result.IsSuccess ? Ok($"User with id {model.Id} updated succesfully!") : (IHttpActionResult)BadRequest(result.Error);
        }

        //Delete User by Id
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than 0");
            }

            var result = _userService.Delete(id);
            return result.IsSuccess ? Ok($"User with id {id} deleted with his setting and statistic succesfully!") : (IHttpActionResult)BadRequest(result.Error);

        }

    }
}
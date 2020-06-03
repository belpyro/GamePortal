using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using FluentValidation;
using FluentValidation.WebApi;
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
        private readonly IValidator<UserSettingDto> _userSettingvalidator;
        private readonly IValidator<UserDto> _userValidator;

        public TTGUsersController(IUserService userService, IValidator<UserSettingDto> UserSettingvalidator, IValidator<UserDto> UserValidator)
        {
            this._userService = userService;
            this._userSettingvalidator = UserSettingvalidator;
            this._userValidator = UserValidator;
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
        public IHttpActionResult GetById([FromUri]int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than 0");
            }

                return _userService.GetById(id) == null ? (IHttpActionResult)NotFound() : Ok(_userService.GetById(id));
             
        }

        //Add new user
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody] UserSettingDto model)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                _userSettingvalidator.ValidateAndThrow(model, "PreValidation");
               return _userService.Add(model) == null ? (IHttpActionResult)Conflict() : Created($"/registerusers/{model.Id}", model);
            }
            catch (ValidationException ex )
            {
                return BadRequest(ex.Message);
            }
            
        }

        //Update User by Id
        [HttpPut]
        [Route("")]
        public IHttpActionResult Update([FromBody]UserDto model)
        {
            try
            {
                _userValidator.ValidateAndThrow(model, "PreValidation");
                _userService.Update(model);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        //Delete User by Id
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0 )
            {
                return BadRequest("ID must be greater than 0");
            }
            try
            {
                _userService.Delete(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch
            {

                return BadRequest("No User with such ID");
            }

        }

    }
}
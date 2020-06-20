using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var result = await _userService.GetAllAsync();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)BadRequest(result.Error);
        }

        //Get Full User Info by Id
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetByIdAsync([FromUri]int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than 0");
            }

            var result = await _userService.GetByIdAsync(id);
            if (result.IsFailure)
                return (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
            return result.Value.HasNoValue ? (IHttpActionResult)NotFound() : Ok(result.Value.Value);

        }

        //Add new user
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> AddAsync([FromBody] UserSettingDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.AddAsync(model);
            return result.IsSuccess ? Created($"/textsets/{result.Value.Id}", result.Value) : (IHttpActionResult)BadRequest(result.Error);
        }

        //Update User by Id
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> UpdateAsync([FromBody]UserDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.UpdateAsync(model);
            return result.IsSuccess ? Ok($"User with id {model.Id} updated succesfully!") : (IHttpActionResult)BadRequest(result.Error);
        }

        //Delete User by Id
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than 0");
            }

            var result =  await _userService.DeleteAsync(id);
            return result.IsSuccess ? Ok($"User with id {id} deleted with his setting and statistic succesfully!") : (IHttpActionResult)BadRequest(result.Error);

        }

    }
}
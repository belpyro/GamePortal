using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using FluentValidation;
using FluentValidation.WebApi;
using JetBrains.Annotations;
using Kbalan.TouchType.Logic.Dto;
using Kbalan.TouchType.Logic.Exceptions;
using Kbalan.TouchType.Logic.Services;
using Kbalan.TouchType.Logic.Validators;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

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

        [HttpPost, Route("register")]
        public async Task<IHttpActionResult> Register([FromBody]NewUserDto model)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid model");
            try
            {
                var result = await _userService.Register(model);

                return result.IsSuccess ? StatusCode(HttpStatusCode.NoContent) : StatusCode(HttpStatusCode.InternalServerError);
            }

            catch (TTGValidationException ex)
            {

                return (IHttpActionResult)BadRequest(ex.Message);
            }

        }

        //Get All RegisterUsers
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var result = await _userService.GetAllAsync();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)BadRequest(result.Error);
        }

        //Get User by Id
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetAsync(string id)
        {
            var result = await _userService.GetAsync(id);
            return result.IsSuccess ? Ok(result.Value.Value) : (IHttpActionResult)BadRequest(result.Error);
        }

        [HttpPost, Route("login")]
        public async Task<IHttpActionResult> Login([FromBody]LoginDto model)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid model");

            var result = await _userService.GetUser(model.UserName, model.Password);
            if (result.HasNoValue) return Unauthorized();

            //ClaimsIdentity
            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, result.Value.Id));
            identity.AddClaim(new Claim(ClaimTypes.Name, result.Value.UserName));

            var provider = Request.GetOwinContext().Authentication;

            provider.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            provider.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);

            return Ok();
        }

        //Delete User by Id
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteAsync(string id)
        {

            var result =  await _userService.DeleteAsync(id);
            return result.IsSuccess ? Ok($"User {id} deleted with his setting and statistic succesfully!") : (IHttpActionResult)BadRequest(result.Error);

        }

    }
}
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

            var result = await _userService.Register(model);

            return result.IsSuccess ? StatusCode(HttpStatusCode.NoContent) : StatusCode(HttpStatusCode.InternalServerError);
        }

        //Get All RegisterUsers
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var result = await _userService.GetAllAsync();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)BadRequest(result.Error);
        }

        [HttpPost, Route("login")]
        public async Task<IHttpActionResult> Login([FromBody]LoginDto model)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid model");

            var result = await _userService.GetUser(model.UserName, model.Password);
            if (result.HasNoValue) return Unauthorized();

            //ClaimsPrincipal
            //ClaimsIdentity
            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, result.Value.Id));
            identity.AddClaim(new Claim(ClaimTypes.Name, result.Value.UserName));

            var provider = Request.GetOwinContext().Authentication;

            // use for AspNet.Identity integration with OWIN pipeline
            //var manager = Request.GetOwinContext().Get<UserManager<IdentityUser>>();
            //var idn = await manager.CreateIdentityAsync(new IdentityUser { }, DefaultAuthenticationTypes.ApplicationCookie);

            provider.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            provider.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);

            return Ok();
        }
        /*        //Get Full User Info by Id
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

                }*/

    }
}
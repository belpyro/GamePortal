using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using AliaksNad.Battleship.Logic.Models;
using AliaksNad.Battleship.Logic.Services;
using AliaksNad.Battleship.Logic.Services.Contracts;
using FluentValidation;
using FluentValidation.WebApi;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace GamePortal.Web.Api.Controllers.Battleship
{
    [RoutePrefix("api/battleship/BSUsers")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserDto> _userDtoValidator;

        public UserController(IUserService userService, IValidator<UserDto> userDtoValidator)
        {
            this._userService = userService;
            this._userDtoValidator = userDtoValidator;
        }

        /// <summary>
        /// Create and register new user in app
        /// </summary>
        /// <param name="model">New user model</param>
        /// <returns></returns>
        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Register([FromBody]NewUserDto model)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid model");

            var result = await _userService.RegisterAsync(model);
            return result.IsSuccess ? StatusCode(HttpStatusCode.NoContent) : StatusCode(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Login and authentication user
        /// </summary>
        /// <param name="model">User name and password</param>
        /// <returns></returns>
        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Login([FromBody]LoginDto model)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid model");

            var result = await _userService.GetUserAsync(model.UserName, model.Password);
            if (result.HasNoValue) return Unauthorized();

            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, result.Value.Id));
            identity.AddClaim(new Claim(ClaimTypes.Name, result.Value.UserName));

            var provider = Request.GetOwinContext().Authentication;

            provider.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            provider.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);

            return Ok();
        }

        /// <summary>
        /// Update user model in app
        /// </summary>
        /// <param name="model">User model</param>
        /// <returns></returns>
        [HttpPut, Route("")]
        public async Task<IHttpActionResult> Update(/*[CustomizeValidator(RuleSet = "PreValidation")]*/[FromBody]UserDto model) // TODO: Check Attribute for validation
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var preValidationResult = _userDtoValidator.Validate(model, ruleSet: "PreValidation");
            if (!preValidationResult.IsValid)
            {
                return BadRequest(preValidationResult.Errors.Select(x => x.ErrorMessage).First());
            }

            var result = await _userService.UpdateAsync(model);
            return result.IsSuccess ? Ok() : (IHttpActionResult)StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Reset user password in app
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns></returns>
        [HttpPost, Route("{email}")]
        public async Task<IHttpActionResult> ResetPassword([FromUri]string email)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid email");

            var result = await _userService.ResetPasswordAsync(email);
            return result.IsSuccess ? Ok() : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Change user password in app
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="token">Validation token</param>
        /// <param name="newPassword">New password</param>
        /// <returns></returns>
        [HttpPost, Route("ChangePass")]
        public async Task<IHttpActionResult> ChangePassword([FromBody]string userId, [FromBody]string token, [FromBody]string newPassword)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid model");

            var result = await _userService.ChangePasswordAsync(userId, token, newPassword);
            return result.IsSuccess ? StatusCode(HttpStatusCode.NoContent) : StatusCode(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Confirm user email in app
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="token">Validation token</param>
        /// <returns></returns>
        [HttpPost, Route("ConfirmEmail")]
        public async Task<IHttpActionResult> ConfirmEmail([FromBody]string userId, string token)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid model");

            var result = await _userService.ConfirmEmailAsync(userId, token);
            return result.IsSuccess ? StatusCode(HttpStatusCode.NoContent) : StatusCode(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Delete user in app
        /// </summary>
        /// <param name="UserDto">User model</param>
        [HttpDelete, Route("{id:int:min(1)}")]
        public async Task<IHttpActionResult> Delete(UserDto model)
        {
            var result = await _userService.DeleteAsync(model);
            return result.IsSuccess ? StatusCode(HttpStatusCode.NoContent) : StatusCode(HttpStatusCode.InternalServerError);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}

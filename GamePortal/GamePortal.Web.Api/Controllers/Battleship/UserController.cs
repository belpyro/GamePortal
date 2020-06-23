using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AliaksNad.Battleship.Logic.Models;
using AliaksNad.Battleship.Logic.Services;
using FluentValidation;
using FluentValidation.WebApi;

namespace GamePortal.Web.Api.Controllers.Battleship
{
    [RoutePrefix("api/battleship/users")]
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
        /// Get all users from logic layer.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var result = _userService.GetAll();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Get user from logic layer by id.
        /// </summary>
        /// <param name="id">user id.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int:min(1)}")]      // TODO: Check Route Constraints 
        public IHttpActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            if (user.IsFailure)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
            return user.Value.HasNoValue ? (IHttpActionResult)NotFound() : Ok(user.Value.Value);
        }

        /// <summary>
        /// Add user to data via logic layer.
        /// </summary>
        /// <param name="model">User model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([CustomizeValidator(RuleSet = "PreValidation")][FromBody]UserDto model) // TODO: Check Attribute for validation
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var preValidationResult = _userDtoValidator.Validate(model, ruleSet: "PreValidation");
            //if (!preValidationResult.IsValid)
            //{
            //    return BadRequest(preValidationResult.Errors.Select(x => x.ErrorMessage).First());
            //}

            var result = _userService.Add(model);
            return result.IsSuccess ? Created($"/api/battleship/users/{result.Value.Id}", result.Value) : (IHttpActionResult)BadRequest(result.Error) ;
            
        }

        /// <summary>
        /// Update user model in data via logic layer.
        /// </summary>
        /// <param name="model">User model.</param>
        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(/*[CustomizeValidator(RuleSet = "PreValidation")]*/[FromBody]UserDto model) // TODO: Check Attribute for validation
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

            var result = _userService.Update(model);
            return result.IsSuccess ? Ok() : (IHttpActionResult)StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete user in data via logic layer by id.
        /// </summary>
        /// <param name="id">User id.</param>
        [HttpDelete]
        [Route("{id:int:min(1)}")]      // TODO: Check Route Constraints 
        public IHttpActionResult Delete(int id)
        {
            _userService.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> Register([FromBody]NewUserDto model)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid model");

            var result = await _userService.Register(model);

            return result.IsSuccess ? StatusCode(HttpStatusCode.NoContent) : StatusCode(HttpStatusCode.InternalServerError);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}

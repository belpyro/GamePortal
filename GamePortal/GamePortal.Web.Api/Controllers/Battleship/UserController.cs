using System.Net;
using System.Web.Http;
using AliaksNad.Battleship.Logic.Models;
using AliaksNad.Battleship.Logic.Services;

namespace GamePortal.Web.Api.Controllers.Battleship
{
    [RoutePrefix("api/battleship/users")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        /// <summary>
        /// Returns all users.
        /// </summary>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        /// <summary>
        /// Returns user by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            return user == null ? (IHttpActionResult)NotFound() : Ok(user);
        }

        /// <summary>
        /// Add user.
        /// </summary>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody]UserDto model)
        {
            model = _userService.Add(model);
            return Created($"/api/battleship/users/{model.Id}", model);
        }

        /// <summary>
        /// Update user by id.
        /// </summary>
        [HttpPut]
        [Route("")]
        public IHttpActionResult Update([FromBody]UserDto model)
        {
            _userService.Update(model);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            _userService.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        } 
    }
}

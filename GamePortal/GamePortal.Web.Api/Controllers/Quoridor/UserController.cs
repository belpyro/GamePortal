using GamePortal.Logic.Igro.Quoridor.Logic.Models;
using GamePortal.Logic.Igro.Quoridor.Logic.Services;
using Igro.Quoridor.Logic.Services;
using Igro.Quoridor.Logic.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace GamePortal.Web.Api.Controllers.Quoridor
{
    /// <summary>
    /// Controller for user
    /// </summary>
    [RoutePrefix("api/Quoridor/User")]
    public class QuorUserController : ApiController
    {
        private readonly IUserService _regUserService;

        public QuorUserController(IUserService regUserService)
        {
            this._regUserService = regUserService;
        }
        /// <summary>
        /// Return all users
        /// </summary>
        /// <returns> List of all players, StatusCode: 200 </returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult ShowAllPlayers()
        {
            return Ok(_regUserService.GetAllUsers());
        }

        /// <summary>
        /// Search player by id
        /// </summary>
        /// <param name="id"> User id </param>
        /// <returns> 
        /// <para> if player was found: Player model, Status Code: 200 </para>
        /// <para> else: Status Code: 404 </para>
        /// Player model </returns>
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid player id");
            var user = _regUserService.GetById(id);
            return user == null ? (IHttpActionResult)NotFound() : Ok(user);
        }

        /// <summary>
        /// Edit profile registered user
        /// </summary>
        /// <param name="id"> Registered user id </param>
        /// <param name="user"> Registered user model </param>
        /// <returns>  Modified model, StatusCode: 200  </returns>
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult EditProfileUsers(int id, [FromBody]UserDTO user)
        {
            _regUserService.EditProfileUsers(id, user);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete account registered user
        /// </summary>
        /// <param name="id"> Registered user id </param>
        /// <returns>
        /// <para> if player was found: Status Code: 204 </para>
        /// <para> else: Status Code: 404 </para>
        /// </returns>
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteProfileUsers(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid player id");
            var find = _regUserService.DeleteProfileUsers(id);
            return find == false ? (IHttpActionResult)NotFound() : StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Register new player
        /// </summary>
        /// <param name="regUser"> Player model </param>
        /// <returns>
        /// <para>  Player model, Status Code: 201 </para>
        /// </returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult RegisterNewPlayer([FromBody]UserDTO regUser)
        {
            if (!ModelState.IsValid)
                return BadRequest("Non Valid");
            var (model, alreadyUse) = _regUserService.RegisterNewPlayer(regUser);
            if (alreadyUse == true)
            {
                return BadRequest($"This email address is already associated to a {model.UserName} user.");
            }
            return Created($"/UserDTO/{model.Id}", regUser);
        }

        /// <summary>
        ///  Verify the username and password with the database
        /// </summary>
        /// <param name="email"> Registered UserDTO Email </param>
        /// <param name="password"> Registered UserDTO Email </param>
        /// <returns>
        /// <para> if player was found: Status Code: 200 </para>
        /// <para> else: Status Code: 401 </para>
        /// </returns>
        [HttpGet]
        [Route("LogIn")]
        public IHttpActionResult LogIn(string email, string password)
        {
            if (email == null || password == null)
                return BadRequest("Not a valid email or password");
            bool findAccount = _regUserService.LogIn(email, password);
            return findAccount != true ? (IHttpActionResult)Unauthorized() : Ok("authorization completed successfully");
        }

        /// <summary>
        /// Restore password
        /// </summary>
        /// <param name="email"> Email registered user </param>
        /// <returns>
        /// if registered user was found: Password, StatusCode: 200 </para>
        /// <para> else: Status Code: 404 </para>
        /// </returns>
        [HttpGet]
        [Route("rePass")]
        public IHttpActionResult RestorePassword([FromUri]string email) 
        {
            var password = _regUserService.RestorePassword(email);
            return password == "Not found" ? (IHttpActionResult)NotFound() : Ok(password);
        }

    }
}
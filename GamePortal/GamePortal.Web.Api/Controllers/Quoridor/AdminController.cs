using GamePortal.Web.Api.Models.Quoridor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GamePortal.Web.Api.Controllers.Quoridor
{
    /// <summary>
    /// Controller for user with administrator rights
    /// </summary>
    [RoutePrefix("api/Quoridor/Admin")]

    public class AdminController : ApiController
    {
        /// <summary>
        /// Return all users
        /// </summary>
        /// <returns> List of all players, StatusCode: 200 </returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult ShowAllPlayers()
        {
            return Ok(RepoFromPrototypes._users);
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
            var user = RepoFromPrototypes._users.FirstOrDefault(x => x.Id == id);
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
        public IHttpActionResult EditProfileUsers(int id, [FromBody]RegPlayer user)
        {
            var oldUser = RepoFromPrototypes._users.FirstOrDefault(x => x.Id == id);
            oldUser.Id = user.Id;
            oldUser.UserName = user.UserName;
            oldUser.FirstName = user.FirstName;
            oldUser.LastName = user.LastName;
            oldUser.Password = user.Password;
            oldUser.Email = user.Email;
            oldUser.DateOfBirth = user.DateOfBirth;
            oldUser.Avatar = user.Avatar;
            return Ok(oldUser);
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
            var user = RepoFromPrototypes._users.FirstOrDefault(x => x.Id == id);
            RepoFromPrototypes._users.Remove(user);
            return user == null ? (IHttpActionResult)NotFound() : StatusCode(HttpStatusCode.NoContent);
            // or Ok($"Delete user with id: {id}");
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
        public IHttpActionResult RegisterNewPlayer([FromBody]RegPlayer regUser)
        {
            var userEmail = RepoFromPrototypes._users.FirstOrDefault(x => x.Email == regUser.Email);
            if (userEmail != null)
            {
                return BadRequest($"This email address is already associated to a {userEmail.UserName} user.");
            }
            var id = RepoFromPrototypes._users.Last().Id + 1;
            regUser.Id = id;
            RepoFromPrototypes._users.Add(regUser);
            return Created($"/User/{id}", regUser);
        }

        /*
        This actions needs to be implemented:
         Watch messeges
         Answer the questions
         View leaderboard
         Edit leaderboard
         */

    }
}
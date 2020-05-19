using GamePortal.Web.Api.Models.Quoridor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;

namespace GamePortal.Web.Api.Controllers.Quoridor
{
    /// <summary>
    /// Controller for unregistered user 
    /// </summary>
    [RoutePrefix("api/Quoridor/UnregUser")]
    public class UnregUserController : ApiController
    {
        /// <summary>
        ///  Verify the username and password with the database
        /// </summary>
        /// <param name="email"> Registered User Email </param>
        /// <param name="password"> Registered User Email </param>
        /// <returns>
        /// <para> if player was found: Status Code: 200 </para>
        /// <para> else: Status Code: 401 </para>
        /// </returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult LogIn(string email, string password)
        {
            if (email == null || password == null)
                return BadRequest("Not a valid player id");
            var user = RepoFromPrototypes._users.FirstOrDefault(x => x.Password == password && x.Email == email);
            return user == null ? (IHttpActionResult)Unauthorized() : Ok("authorization completed successfully");
        }


        /// <summary>
        /// Register new player
        /// </summary>
        /// <param name="regUser"> Player model </param>
        /// <returns>
        /// <para>  Player model, StatusCode: 201 </para>
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

        /// <summary>
        /// Restore password
        /// </summary>
        /// <param name="email"> Email registered user </param>
        /// <returns>
        /// if registered user was found: Password, StatusCode: 200 </para>
        /// <para> else: Status Code: 404 </para>
        /// </returns>
        [HttpGet]
        [Route("{email}")]
        public IHttpActionResult RestorePassword(string email)
        {
            var user = RepoFromPrototypes._users.FirstOrDefault(x => x.Email == email);
            return user == null ? (IHttpActionResult)NotFound() : Ok(user.Password);
        }

        /// <summary>
        /// View leaderboard
        /// </summary>
        /// <returns> dictionary containing statistics of the best players, StatusCode: 200 </returns>
        [HttpGet]
        [Route("Leaderboard")]
        public IHttpActionResult UserViewLeaderboard()
        {
            string viewLeader = null;
            foreach (var v in RepoFromPrototypes._leaderBoard)
            {
                string viewLeaderBoard = $"{v.Key.UserName} - {v.Value}  ";
                viewLeader += viewLeaderBoard;
            }
            return Ok(viewLeader);
        }
    }
}
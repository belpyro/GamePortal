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
    [RoutePrefix("api/Quoridor/UnregUser")]
    public class UnregUserController : ApiController
    {
        // Log in
        [HttpGet]
        [Route("LogIn")]
        public IHttpActionResult LogIn(string email, string password)
        {
            if (email == null || password == null)
                return BadRequest("Not a valid player id");
            var user = RepoFromPrototypes._users.FirstOrDefault(x => x.Password == password && x.Email == email);
            return user == null ? (IHttpActionResult)Unauthorized() : Ok("authorization completed successfully");
        }
        
        // Register new account
        [HttpPost]
        [Route("regUser")]
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

        // Restore password
        [HttpGet]
        [Route("rePassword")]
        public IHttpActionResult RestorePassword(string rePassword)
        {
            var user = RepoFromPrototypes._users.FirstOrDefault(x => x.Email == rePassword);
            return user == null ? (IHttpActionResult)NotFound() : Ok(user.Password);
        }

        // View leaderboard
        [HttpGet]
        [Route("ViewLeaderboard")]
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
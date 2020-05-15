using GamePortal.Web.Api.Models.Quoridor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GamePortal.Web.Api.Controllers.Quoridor
{
    public class RegUserController : ApiController
    {
        // Log in
        [HttpGet]
        [Route("LogIn")]
        public IHttpActionResult LogIn(string loggin, string password)
        {
            var user = RepoFromPrototypes._users.FirstOrDefault(x => x.Password == password && x.Loggin == loggin);
            return user == null ? (IHttpActionResult)BadRequest("Invalid Loggin/Password!") : Ok("authorization completed successfully");
        }
        // Log out
        [HttpGet]
        [Route("LogOut")]
        public IHttpActionResult LogOut()
        {
            return Redirect("https://localhost:44313/swagger/ui/index#!/RegUser/RegUser_LogIn");
        }
        // Delete account
        // Open settings panel
        // Change move timeout
        // Enable the function "random fences"
        // Open edit profile panel
        // Edit name
        // Change pasword
        // Edit email
        // Edit pawn avatar
        // View leaderboard
        // Select game type
    }
}
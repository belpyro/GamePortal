using GamePortal.Web.Api.Models.Quoridor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GamePortal.Web.Api.Controllers.Quoridor
{
    [RoutePrefix("api/Quoridor/RegUser")]
    public class RegUserController : ApiController
    {
        // Log out
        [HttpGet]
        [Route("LogOut")]
        public IHttpActionResult LogOut()
        {
            return Redirect("https://localhost:44313/swagger/ui/index#!/UnregUser/UnregUser_LogIn");
        }
        // Delete account

        // Open settings panel
        [HttpGet]
        [Route("OpenSettings")]
        public IHttpActionResult OpenSettings()
        {
            return Ok();
        }
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
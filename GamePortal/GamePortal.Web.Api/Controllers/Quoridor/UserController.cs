using GamePortal.Logic.Igro.Quoridor.Logic.Models;
using GamePortal.Logic.Igro.Quoridor.Logic.Services;
using Igro.Quoridor.Logic.Services;
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
        private readonly IRegUserService _regUserService;
        public UnregUserController(IRegUserService regUserService)
        {
            this._regUserService = regUserService;
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
        [Route("")]
        public IHttpActionResult LogIn(string email, string password)
        {
            if (email == null || password == null)
                return BadRequest("Not a valid player id");
            bool findAccount = _regUserService.LogIn(email, password);
            return findAccount != true ? (IHttpActionResult)Unauthorized() : Ok("authorization completed successfully");
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
        public IHttpActionResult RegisterNewPlayer([FromBody]RegPlayerDTO regUser)
        {
            var userEmail = _regUserService.GetAllUsers().FirstOrDefault(x => x.Email == regUser.Email);
            if (userEmail != null)
            {
                return BadRequest($"This email address is already associated to a {userEmail.UserName} user.");
            }
            _regUserService.RegisterNewPlayer(regUser);
            var id = regUser.Id;
            return Created($"/UserDTO/{id}", regUser);
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
            var password = _regUserService.RestorePassword(email);
            return password == null ? (IHttpActionResult)NotFound() : Ok(password);
        }

        /// <summary>
        /// View leaderboard
        /// </summary>
        /// <returns> dictionary containing statistics of the best players, StatusCode: 200 </returns>
        //[HttpGet]
        //[Route("Leaderboard")]
        //public IHttpActionResult UserViewLeaderboard()
        //{
        //    string viewLeader = null;
        //    foreach (var v in QuoridorService._leaderBoard)
        //    {
        //        string viewLeaderBoard = $"{v.Key.UserName} - {v.Value}  ";
        //        viewLeader += viewLeaderBoard;
        //    }
        //    return Ok(viewLeader);
        //}
    }
}
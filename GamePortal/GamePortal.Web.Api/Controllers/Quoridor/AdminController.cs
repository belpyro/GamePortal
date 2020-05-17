using GamePortal.Web.Api.Models.Quoridor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GamePortal.Web.Api.Controllers.Quoridor
{
    [RoutePrefix("api/Quoridor/Admin")]
    public class AdminController : ApiController
    {
        // Show all users
        [HttpGet]
        [Route("ShowAllPlayers")]
        public IHttpActionResult ShowAllPlayers()
        {
            return Ok(RepoFromPrototypes._users);
        }
        
        //Search player by id
        [HttpGet]
        [Route("SearchPlayer/{id}")]
        public IHttpActionResult GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid player id");
            var user = RepoFromPrototypes._users.FirstOrDefault(x => x.Id == id);
            return user == null ? (IHttpActionResult)NotFound() : Ok(user);
        }

        // Edit profile RegUser
        [HttpPut]
        [Route("EditProfileUser/{id}")]
        public IHttpActionResult  EditProfileUsers(int id, [FromBody]RegPlayer user)
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

        // Delete account RegUser
        [HttpDelete]
        [Route("DeletePlayer/{id}")]
        public IHttpActionResult DeleteProfileUsers(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid player id");
            var user = RepoFromPrototypes._users.FirstOrDefault(x => x.Id == id);
            RepoFromPrototypes._users.Remove(user);
            return user == null ? (IHttpActionResult)NotFound() : StatusCode(HttpStatusCode.NoContent); 
            // or Ok($"Delete user with id: {id}");
        }

        // Register new player
        [HttpPost]
        [Route("RegNewPlayer")]
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

        // Log out
        [HttpGet]
        [Route("LogOut")]
        public IHttpActionResult LogOut()
        {
            return Redirect("https://localhost:44313/swagger/ui/index#!/UnregUser/UnregUser_LogIn");
        }
        // Watch messeges
        // Answer the questions
        // View leaderboard
        // Edit leaderboard

    }
}
using GamePortal.Web.Api.Models.Quoridor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GamePortal.Web.Api.Controllers.Quoridor
{
    [RoutePrefix("Quoridor")]
    public class AdminController : ApiController
    {
        // Log in
        [HttpGet]
        [Route("LogIn")]
        public IHttpActionResult LogIn(string loggin, string password)
        {
        if (loggin == null || password == null)
                return BadRequest("Not a valid player id");
            var user = RepoFromPrototypes._users.FirstOrDefault(x => x.Password == password && x.Loggin == loggin);
            return user == null ? (IHttpActionResult)Unauthorized() : Ok("authorization completed successfully");
        }

        // Show all users
        [HttpGet]
        [Route("getAll")]
        public IHttpActionResult GetAll()
        {
            return Ok(RepoFromPrototypes._users);
        }
        
        //Search player by id
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid player id");
            var user = RepoFromPrototypes._users.FirstOrDefault(x => x.Id == id);
            return user == null ? (IHttpActionResult)NotFound() : Ok(user);
        }

        // Edit profile RegUser
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult  EditProfileUsers(int id, [FromBody]RegPlayer user)
        {
            var oldUser = RepoFromPrototypes._users.FirstOrDefault(x => x.Id == id);
            oldUser.Id = user.Id;
            oldUser.UserName = user.UserName;
            oldUser.FirstName = user.FirstName;
            oldUser.LastName = user.LastName;
            oldUser.Loggin = user.Loggin;
            oldUser.Password = user.Password;
            oldUser.Email = user.Email;
            oldUser.DateOfBirth = user.DateOfBirth;
            oldUser.Avatar = user.Avatar;
            //RepoFromPrototypes._users.Remove(oldUser);
            //RepoFromPrototypes._users.Add(user);
            return Ok(oldUser);
        }

        //Delete account RegUser
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteProfileUsers(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid player id");
            var user = RepoFromPrototypes._users.FirstOrDefault(x => x.Id == id);
            RepoFromPrototypes._users.Remove(user);
            return user == null ? (IHttpActionResult)NotFound() : Ok($"Delete user with id: {id}");
        }

        // Register new player
        [HttpPost]
        [Route("regUser")]
        public IHttpActionResult RegisterNewPlayer([FromBody]RegPlayer regUser)
        {
            var id = RepoFromPrototypes._users.Last().Id + 1;
            regUser.Id = id;
            RepoFromPrototypes._users.Add(regUser);
            return Created($"/User/{id}", regUser);
        }

        // Log out
        // Watch messeges
        // Answer the questions
        // View leaderboard
        // Edit leaderboard

    }
}
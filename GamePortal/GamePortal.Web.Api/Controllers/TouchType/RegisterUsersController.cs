using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Kbalan.TouchType.Logic.Dto;
namespace GamePortal.Web.Api.Controllers.TouchType
{
    [RoutePrefix("api/registerusers")]
    public class RegisterUsersController : ApiController
    {
        private static List<RegisterUserDto> _registerUsers = new List<RegisterUserDto>
        {
            new RegisterUserDto
            {
                Id = 1,
                NickName = "firstUser",
                Avatar = "pathToId1Ava",
                Password = "1234",
                LevelOfGame = 1,
                Role = "admin",
                MaxSpeedRecord = 50,
                NumberOfGamesPlayed = 329
            },
            new RegisterUserDto
            {
                Id = 2,
                NickName = "secondUser",
                Avatar = "pathToId2Ava",
                Password = "1111",
                LevelOfGame = 3,
                Role = "user",
                MaxSpeedRecord = 43,
                NumberOfGamesPlayed = 215
            }
        };

        //Get All RegisterUsers
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_registerUsers);
        }

        //Get User by Id
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int Id)
        {
            var registerUser = _registerUsers.FirstOrDefault(x => x.Id == Id);
            return registerUser == null ? (IHttpActionResult)NotFound() : Ok(registerUser);
        }

        //Add new user
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody]RegisterUserDto model)
        {
            var id = _registerUsers.Last().Id + 1;
            model.Id = id;
            _registerUsers.Add(model);
            return Created($"/registerusers/{id}", model);
        }

        //Update User by Id
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update(int id, [FromBody]RegisterUserDto model)
        {
                for (int i = 0; i < _registerUsers.Count; i++)
                {
                    if (_registerUsers[i].Id == id)
                    {
                        _registerUsers[i] = model;
                        _registerUsers[i].Id =id;                
                        return Created($"/registerusers/{id}", _registerUsers[i]);
                    }                       
                }
                return NotFound();
        }

        //Delete User by Id
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            for (int i = 0; i < _registerUsers.Count; i++)
            {
                if (_registerUsers[i].Id == id)
                {
                    _registerUsers.RemoveAt(i);
                    return Ok();
                }
            }
            return NotFound();
        }

    }
}
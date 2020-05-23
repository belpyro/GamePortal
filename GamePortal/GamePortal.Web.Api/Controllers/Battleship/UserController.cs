using AliaksNad.Battleship.Logic.DTO;
using AliaksNad.Battleship.Logic.DB;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AliaksNad.Battleship.Logic.Models;

namespace GamePortal.Web.Api.Controllers.Battleship
{
    [RoutePrefix("api/battleship/users")]
    public class UserController : ApiController
    {
        protected DataBase _db = new DataBase();

        /// <summary>
        /// Returns all users.
        /// </summary>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_db.GetUsers());
        }

        ///// <summary>
        ///// Returns user by id.
        ///// </summary>
        //[HttpGet]
        //[Route("{id}")]
        //public IHttpActionResult GetById(int id)
        //{
        //    var user = _db.GetUsers().FirstOrDefault(x => x.Id == id);
        //    return user == null ? (IHttpActionResult)NotFound() : Ok(user);
        //}

        ///// <summary>
        ///// Update user by id.
        ///// </summary>
        //[HttpPut]
        //[Route("")]
        //public IHttpActionResult UpdateById([FromBody]UserDto model)
        //{
        //    var user = _db.GetUsers().FirstOrDefault(x => x.Id == model.Id);
        //    if (user != null)
        //    {
        //        _db.UpdateUser(model);
        //        return Ok();
        //    }
        //    return NotFound();
        //}
    }
}

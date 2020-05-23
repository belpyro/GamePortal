using AliaksNad.Battleship.Logic.DB;
using AliaksNad.Battleship.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GamePortal.Web.Api.Controllers.Battleship
{
    [RoutePrefix("api/battlesip/registration")]
    public class RegistrationController : ApiController
    {
        protected DataBase _db = new DataBase();

        /// <summary>
        /// Registration users in database.
        /// </summary>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Registration([FromBody]UserDto model)
        {
            var user = _db.GetUsers().FirstOrDefault(x => x.Email == model.Email);
            if (user == null)
            {
                model.Id = _db.GetUsers().Last().Id + 1;
                _db.GetUsers().Add(model);
                return Created($"/users/{model.Id}", model);
            }
            return BadRequest("Email already registed.");
        }
    }
}

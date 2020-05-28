using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GamePortal.Web.Api.Controllers.Battleship
{
    //[RoutePrefix("api/battlesip/login")]
    //public class LogInController : ApiController
    //{
    //    protected DataBase _db = new DataBase();

    //    /// <summary>
    //    /// Checks email and password for compliance.
    //    /// </summary>
    //    [HttpPost]
    //    [Route("")]
    //    public IHttpActionResult LogIn([FromBody]LogInDTO validation)
    //    {
    //        var user = _db.GetUsers().FirstOrDefault(x => x.Email == validation.Email && x.Password == validation.Password);
    //        return user == null ? (IHttpActionResult)NotFound() : Ok(user);
    //    }
    //}
}

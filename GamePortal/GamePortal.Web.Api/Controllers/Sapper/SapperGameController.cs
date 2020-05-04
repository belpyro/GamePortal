using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GamePortal.Web.Api.Controllers.Sapper
{
    public class SapperGameController : ApiController
    {
        [HttpGet, Route("api/SapperGame/test/result")]
        public IHttpActionResult TestResultOk()
        {
            return Ok();
        }
    }
}
using AliaksNad.Battleship.Logic.DB;
using AliaksNad.Battleship.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GamePortal.Web.Api.Controllers.Battleship
{
    [RoutePrefix("api/battlesip/statistic")]
    public class StatisticController : ApiController
    {
        protected DataBase _db = new DataBase();

        /// <summary>
        /// Returns all users statistics.
        /// </summary>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var stat = _db.GetStatistic().OrderByDescending(x => x.Score);
            return stat == null ? (IHttpActionResult)NotFound() : Ok(stat);
        }

        /// <summary>
        /// Returns all user statistics by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var stat = _db.GetStatistic().Where(x => x.Id == id);
            return stat == null ? (IHttpActionResult)NotFound() : Ok(stat);
        }
    }
}

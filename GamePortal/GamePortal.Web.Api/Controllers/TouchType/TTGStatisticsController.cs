using Kbalan.TouchType.Logic.Dto;
using Kbalan.TouchType.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GamePortal.Web.Api.Controllers.TouchType
{
    /// <summary>
    /// Controller for User Statistic
    /// </summary>
    [RoutePrefix("api/statistic")]
    public class TTGStatisticsController : ApiController
    {
        private readonly IStatisticService _statisticService;
        public TTGStatisticsController(IStatisticService statisticService)
        {
            this._statisticService = statisticService;
        }

        //Get All Statistic with user
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_statisticService.GetAll());
        }

        //Get Statistic Info by user Id
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetAllById([FromUri]int Id)
        {
            return _statisticService.GetById(Id) == null ? (IHttpActionResult)NotFound() : Ok(_statisticService.GetById(Id));
        }

        //Update User Statistic by User Id
        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(int id, [FromBody]StatisticDto model)
        {
            _statisticService.Update(id, model);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}

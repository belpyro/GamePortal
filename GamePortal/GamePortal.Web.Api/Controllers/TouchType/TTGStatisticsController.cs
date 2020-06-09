using FluentValidation;
using JetBrains.Annotations;
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

        public TTGStatisticsController([NotNull]IStatisticService statisticService)
        {
            this._statisticService = statisticService;
        }

        //Get All Statistic with user
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var result = _statisticService.GetAll();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)BadRequest(result.Error);
        }

        //Get Statistic Info by user Id
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetAllById([FromUri]int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than 0");
            }
            var result = _statisticService.GetById(id);
            if (result.IsFailure)
                return (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
            return result.Value.HasNoValue ? (IHttpActionResult)NotFound() : Ok(result.Value.Value);
        }

        //Update User Statistic by User Id
        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(int id, [FromBody]StatisticDto model)
        {
            var result = _statisticService.Update(id, model);
            return result.IsSuccess ? Ok($"Statistic of user with id {id} updated succesfully!") : (IHttpActionResult)BadRequest(result.Error);

        }
    }
}

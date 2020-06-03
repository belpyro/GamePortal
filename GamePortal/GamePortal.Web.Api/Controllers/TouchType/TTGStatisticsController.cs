using FluentValidation;
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
        private readonly IValidator<StatisticDto> _statisticValidator;

        public TTGStatisticsController(IStatisticService statisticService, IValidator<StatisticDto> StatisticValidator)
        {
            this._statisticService = statisticService;
            _statisticValidator = StatisticValidator;
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
        public IHttpActionResult GetAllById([FromUri]int id)
        {
            return _statisticService.GetById(id) == null ? (IHttpActionResult)NotFound() : Ok(_statisticService.GetById(id));
        }

        //Update User Statistic by User Id
        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(int id, [FromBody]StatisticDto model)
        {
            try
            {
                _statisticValidator.ValidateAndThrow(model, "PreValidation");
                _statisticService.Update(id, model);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}

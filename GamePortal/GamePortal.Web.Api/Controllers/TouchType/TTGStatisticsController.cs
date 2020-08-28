using FluentValidation;
using GamePortal.Web.Api.Hubs;
using JetBrains.Annotations;
using Kbalan.TouchType.Logic.Dto;
using Kbalan.TouchType.Logic.Exceptions;
using Kbalan.TouchType.Logic.Services;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GamePortal.Web.Api.Controllers.TouchType
{
    /// <summary>
    /// Controller for User Statistic
    /// </summary>
    [RoutePrefix("api/statistic")]
    [System.Web.Http.Authorize]
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
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var result = await _statisticService.GetAllAsync();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)BadRequest(result.Error);
        }

        //Get Statistic Info by user Id
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetAllByIdAsync([FromUri]string id)
        {
            var result = await _statisticService.GetByIdAsync(id);
            if (result.IsFailure)
                return (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
            return result.Value.HasNoValue ? (IHttpActionResult)NotFound() : Ok(result.Value.Value);
        }

        //Update User Statistic by User Id
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> UpdateAsync ([FromBody]StatisticDto model)
        {

            try
            {
                var result = await _statisticService.UpdateAsync( model);
                return result.IsSuccess ? Ok($"Statistic of user with id {model.StatisticId} updated succesfully!") : (IHttpActionResult)BadRequest(result.Error);
            }
            catch (TTGValidationException ex)
            {

                return (IHttpActionResult)BadRequest(ex.Message);
            }
        }
    }
}

using JetBrains.Annotations;
using Kbalan.TouchType.Logic.Dto;
using Kbalan.TouchType.Logic.Exceptions;
using Kbalan.TouchType.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GamePortal.Web.Api.Controllers.TouchType
{
    [RoutePrefix("api/singlegame")]
    public class TTGSingleGameController : ApiController
    {
        private readonly ISingleGameService _singleGameService;


        public TTGSingleGameController([NotNull]ISingleGameService singleGameService)
        {
            this._singleGameService = singleGameService;
        }

        /// <summary>
        /// Method for starting new game. 
        /// Create a new game in Db and send info about it to client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> AddNewSingleGameAsync(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _singleGameService.AddNewSingleGameAsync(id);
                return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)BadRequest(result.Error);
            }
            catch (TTGValidationException ex)
            {
                return (IHttpActionResult)BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Method for handling user turn(each user click on keyboard during game is playing)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> UserTurnAsync([FromBody]NewSingleGameDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _singleGameService.UserTurnAsync(model.Id, model.TextForTyping);
                return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)BadRequest(result.Error);
            }
            catch (TTGValidationException ex)
            {
                return (IHttpActionResult)BadRequest(ex.Message);
            }

        }

    }
}

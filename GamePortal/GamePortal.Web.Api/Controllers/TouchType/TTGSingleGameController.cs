using JetBrains.Annotations;
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
    public class TTGSingleGameController : ApiController
    {
        private readonly ISingleGameService _singleGameService;


        public TTGSingleGameController([NotNull]ISingleGameService singleGameService)
        {
            this._singleGameService = singleGameService;
        }

        //Add new SingleGame
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

    }
}

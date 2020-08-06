using FluentValidation;
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
    /// <summary>
    /// Controller for User Settings
    /// </summary>
    [RoutePrefix("api/settings")]
    public class TTGSettingsController : ApiController
    {
        private readonly ISettingService _settingService;


        public TTGSettingsController([NotNull] ISettingService settingService) 
        {
            this._settingService = settingService;
        }

        //Get All Settings with user
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var result = await _settingService.GetAllAsync();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }

        //Get single Setting Info by User Id
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetByIdAsync([FromUri]string id)
        {
            var result = await _settingService.GetByIdAsync(id);
            if (result.IsFailure)
                return (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
            return result.Value.HasNoValue ? (IHttpActionResult)NotFound() : Ok(result.Value.Value);
        }

        //Update single setting by User Id
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> UpdateAsync(string id ,[FromBody]SettingDto model)
        {
            try
            {
                var result = await _settingService.UpdateAsync(id, model);
                return result.IsSuccess ? Ok($"Settings of user with id {id} updated succesfully!") : (IHttpActionResult)BadRequest(result.Error);
            }
            catch (TTGValidationException ex)
            {

                return (IHttpActionResult)BadRequest(ex.Message);
            }

        }
    }
}

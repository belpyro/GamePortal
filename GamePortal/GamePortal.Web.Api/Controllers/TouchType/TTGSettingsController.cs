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
    /// Controller for User Settings
    /// </summary>
    [RoutePrefix("api/settings")]
    public class TTGSettingsController : ApiController
    {
        private readonly ISettingService _settingService;
        private readonly IValidator<SettingDto> _settingValidator;

        public TTGSettingsController(ISettingService settingService, IValidator<SettingDto> SettingValidator)
        {
            this._settingService = settingService;
            _settingValidator = SettingValidator;
        }

        //Get All Settings with user
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_settingService.GetAll());
        }

        //Get single Setting Info by User Id
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById([FromUri]int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than 0");
            }
            return _settingService.GetById(id) == null ? (IHttpActionResult)NotFound() : Ok(_settingService.GetById(id));
        }

        //Update single setting by User Id
        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(int id ,[FromBody]SettingDto model)
        {
            try
            {
                _settingValidator.ValidateAndThrow(model, "PreValidation");
                _settingService.Update(id, model);
                 return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
    }
}

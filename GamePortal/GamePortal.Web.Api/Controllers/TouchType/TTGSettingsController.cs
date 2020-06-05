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
    /// Controller for User Settings
    /// </summary>
    [RoutePrefix("api/settings")]
    public class TTGSettingsController : ApiController
    {
        private readonly ISettingService _settingService;
        private readonly IValidator<SettingDto> _settingValidator;

        public TTGSettingsController([NotNull] ISettingService settingService, [NotNull]IValidator<SettingDto> SettingValidator)
        {
            this._settingService = settingService;
            _settingValidator = SettingValidator;
        }

        //Get All Settings with user
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var result = _settingService.GetAll();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)BadRequest(result.Error);
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
            var result = _settingService.GetById(id);
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)BadRequest(result.Error);
        }

        //Update single setting by User Id
        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(int id ,[FromBody]SettingDto model)
        {
            var preValidResult = _settingValidator.Validate(model, ruleSet: "PreValidation");
            if (!preValidResult.IsValid)
            {
                return BadRequest(preValidResult.Errors.Select(x => x.ErrorMessage).First());
            }

            var result = _settingService.Update(id, model);
            return result.IsSuccess ? Ok($"Settings of user with id {id} updated succesfully!") : (IHttpActionResult)BadRequest(result.Error);
        }
    }
}

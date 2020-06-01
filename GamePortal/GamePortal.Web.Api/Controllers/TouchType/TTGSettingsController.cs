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
        public TTGSettingsController(ISettingService settingService)
        {
            this._settingService = settingService;
        }

        //Get All Settings with user
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_settingService.GetAll());
        }

        //Get Full Statistic Info by User Id
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetAllById([FromUri]int Id)
        {
            return _settingService.GetById(Id) == null ? (IHttpActionResult)NotFound() : Ok(_settingService.GetById(Id));
        }

        //Update User Statistic by User Id
        [HttpPut]
        [Route("")]
        public IHttpActionResult Update(int id ,[FromBody]SettingDto model)
        {
            _settingService.Update(id, model);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}

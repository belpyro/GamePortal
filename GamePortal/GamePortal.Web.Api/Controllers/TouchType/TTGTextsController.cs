using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Kbalan.TouchType.Logic.Dto;
using Kbalan.TouchType.Logic.Services;

namespace GamePortal.Web.Api.Controllers.TouchType
{
    [RoutePrefix("api/textsets")]
    public class TTGTextsController : ApiController
    {
        private readonly ITextSetService _textSetService;

        public TTGTextsController(ITextSetService textSetService)
        {
            this._textSetService = textSetService;
        }

        //Get All TextSets
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_textSetService.GetAll());
        }

        //Get TextSet by Id
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            return _textSetService.GetById(id) == null ? (IHttpActionResult)NotFound() : Ok(_textSetService.GetById(id));
        }

        ///Get Random TextSet by Level of the text
        [HttpGet]
        [Route("searchbylevel/{level}")]
        public IHttpActionResult GetRandomByLevel(int level)
        {
            return _textSetService.GetByLevel(level) == null ? (IHttpActionResult)NotFound() : Ok(_textSetService.GetByLevel(level));
        }

        //Add new text
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody]TextSetDto model)
        {
            return _textSetService.Add(model) == null ? (IHttpActionResult)Conflict() : Created($"/textsets/{model.Id}", model);
        }

        //Update Text by Id
        [HttpPut]
        [Route("")]
        public IHttpActionResult Update([FromBody]TextSetDto model)
        {
            _textSetService.Update(model);
            return StatusCode(HttpStatusCode.NoContent);
        }

        //Delete Text by Id
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            _textSetService.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}

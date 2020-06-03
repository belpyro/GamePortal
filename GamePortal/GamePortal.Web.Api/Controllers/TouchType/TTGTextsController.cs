using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FluentValidation;
using Kbalan.TouchType.Logic.Dto;
using Kbalan.TouchType.Logic.Services;

namespace GamePortal.Web.Api.Controllers.TouchType
{
    /// <summary>
    /// Controller for TextSet
    /// </summary>
    [RoutePrefix("api/textsets")]
    public class TTGTextsController : ApiController
    {
        private readonly ITextSetService _textSetService;
        private readonly IValidator<TextSetDto> _textSetValidator;

        public TTGTextsController(ITextSetService textSetService, IValidator<TextSetDto> TextSetValidator)
        {
            this._textSetService = textSetService;
            _textSetValidator = TextSetValidator;
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
            if (id <= 0)
            {
                return BadRequest("ID must be greater than 0");
            }

            return _textSetService.GetById(id) == null ? (IHttpActionResult)NotFound() : Ok(_textSetService.GetById(id));
        }

        ///Get Random TextSet by Level of the text
        [HttpGet]
        [Route("searchbylevel/{level}")]
        public IHttpActionResult GetRandomByLevel(int level)
        {
            if (level < 0 || level > 2)
            {
                return BadRequest("Level must be Easy, Middle or Hard");
            }
            return _textSetService.GetByLevel(level) == null ? (IHttpActionResult)NotFound() : Ok(_textSetService.GetByLevel(level));
        }

        //Add new text
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody]TextSetDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                _textSetValidator.ValidateAndThrow(model, "PreValidation");
                return _textSetService.Add(model) == null ? (IHttpActionResult)Conflict() : Created($"/textsets/{model.Id}", model);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        //Update Text by Id
        [HttpPut]
        [Route("")]
        public IHttpActionResult Update([FromBody]TextSetDto model)
        {

            try
            {
                _textSetValidator.ValidateAndThrow(model, "PreValidation");
                _textSetService.Update(model);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //Delete Text by Id
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than 0");
            }
            try
            {
                _textSetService.Delete(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch
            {

                return BadRequest("No User with such ID");
            }
        }
    }
}

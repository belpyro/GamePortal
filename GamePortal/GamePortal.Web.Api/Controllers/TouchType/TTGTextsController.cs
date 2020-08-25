using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CSharpFunctionalExtensions;
using FluentValidation;
using JetBrains.Annotations;
using Kbalan.TouchType.Logic.Dto;
using Kbalan.TouchType.Logic.Services;
using Kbalan.TouchType.Logic.Exceptions;

namespace GamePortal.Web.Api.Controllers.TouchType
{
    /// <summary>
    /// Controller for TextSet
    /// </summary>
    [RoutePrefix("api/textsets")]
    [Authorize]
    public class TTGTextsController : ApiController
    {
        private readonly ITextSetService _textSetService;

        public TTGTextsController([NotNull]ITextSetService textSetService)
        {
            this._textSetService = textSetService;
        }

        //Get All TextSets
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var result = await _textSetService.GetAllAsync();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }

        //Get TextSet by Id
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than 0");
            }

            var result = await _textSetService.GetByIdAsync(id);
            if (result.IsFailure)
                return (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
            return result.Value.HasNoValue ? (IHttpActionResult)NotFound() :  Ok(result.Value.Value);
        }

        ///Get Random TextSet by Level of the text random
        [HttpGet]
        [Route("searchbylevelrand/{id}")]
        public async Task<IHttpActionResult> GetRandomByLevelAsync(string id)
        {

            var result = await _textSetService.GetByLevelAsyncRandom(id);
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)BadRequest(result.Error);

        }

        ///Get Random TextSet by Level of the text
        [HttpGet]
        [Route("searchbylevel/{level}")]
        public async Task<IHttpActionResult> GetByLevelAsync(int level)
        {
            if (level < 0 || level > 2)
            {
                return BadRequest("Level must be Easy, Middle or Hard");
            }

            var result = await _textSetService.GetByLevelAsync(level);
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)BadRequest(result.Error);

        }

        //Add new text
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> AddAsync([FromBody]TextSetDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _textSetService.AddAsync(model);
                return result.IsSuccess ? Created($"/textsets/{result.Value.Id}", result.Value) : (IHttpActionResult)BadRequest(result.Error);
            }
            catch (TTGValidationException ex)
            {

                return (IHttpActionResult)BadRequest(ex.Message);
            }

        }

        //Update Text by Id 
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> UpdateAsync([FromBody]TextSetDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _textSetService.UpdateAsync(model);
                return result.IsSuccess ? Ok($"Text set with id {model.Id} updated succesfully!") : (IHttpActionResult)BadRequest(result.Error);
            }
            catch (TTGValidationException ex)
            {

                return (IHttpActionResult)BadRequest(ex.Message);
            }

        }

        //Delete Text by Id
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than 0");
            }
            var result = await _textSetService.DeleteAsync(id);
            return result.IsSuccess ? Ok($"Text with id {id} successfully deleted") : (IHttpActionResult)BadRequest(result.Error);
        }
    }
}

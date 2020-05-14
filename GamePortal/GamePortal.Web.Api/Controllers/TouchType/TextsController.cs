using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Kbalan.TouchType.Logic.Dto;
namespace GamePortal.Web.Api.Controllers.TouchType
{
    [RoutePrefix("api/textsets")]
    public class TextsController : ApiController
    {
        private static List<TextSetDto> _textSets = new List<TextSetDto>
        {
            new TextSetDto
            {
                Id = 1,
                LevelOfText = 1,
                TextForTyping = "It's easy text for touch typing"
            },
            new TextSetDto
            {
                Id = 2,
                LevelOfText = 2,
                TextForTyping = "It's medium text for touch typing"
            },
        };

        //Get All TextSets
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_textSets);
        }

        //Get User by Id
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int Id)
        {
            var textSet = _textSets.FirstOrDefault(x => x.Id == Id);
            return textSet == null ? (IHttpActionResult)NotFound() : Ok(textSet);
        }

        //Add new text
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody]TextSetDto model)
        {
            var id = _textSets.Last().Id + 1;
            model.Id = id;
            _textSets.Add(model);
            return Created($"/textsets/{id}", model);
        }

        //Update Text by Id
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update(int id, [FromBody]TextSetDto model)
        {
            for (int i = 0; i < _textSets.Count; i++)
            {
                if (_textSets[i].Id == id)
                {
                    _textSets[i] = model;
                    _textSets[i].Id = id;
                    return Created($"/textsets/{id}", _textSets[i]);
                }
            }
            return NotFound();
        }

        //Delete Text by Id
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            for (int i = 0; i < _textSets.Count; i++)
            {
                if (_textSets[i].Id == id)
                {
                    _textSets.RemoveAt(i);
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}

using AliaksNad.Battleship.Logic.Models;
using AliaksNad.Battleship.Logic.Services;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace GamePortal.Web.Api.Controllers.Battleship
{
    [RoutePrefix("api/battleship/game")]
    public class BattleshipGameController : ApiController
    {
        private readonly IGameService _gameService;

        public BattleshipGameController(IGameService gameService)
        {
            this._gameService = gameService;
        }

        /// <summary>
        /// Set your own fleet coordinates on logic layer.
        /// </summary>
        /// <param name="BattleAreaDtoCoordinates">Own fleet coordinates.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("fleets")]
        public IHttpActionResult SetFleet([FromBody]BattleAreaDto BattleAreaDtoCoordinates)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _gameService.SetFleet(BattleAreaDtoCoordinates);
            return result.IsSuccess ? Created($"api/battleship/game/fleets{result.Value.BattleAreaId}", result.Value) : (IHttpActionResult)BadRequest(result.Error);
        }

        /// <summary>
        /// Get all battle area from logic layer.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var result = _gameService.GetAll();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Get battle area from logic layer by id.
        /// </summary>
        /// <param name="id">battle area id.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int:min(1)}")]      // TODO: Check Route Constraints 
        public IHttpActionResult GetById(int id)
        {
            var battleArea = _gameService.GetById(id);
            if (battleArea.IsFailure)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
            return battleArea.Value.HasNoValue ? (IHttpActionResult)NotFound() : Ok(battleArea.Value.Value);
        }

        /// <summary>
        /// Checking hit by enemy coordinates on logic layer.
        /// </summary>
        /// <param name="coordinatesOfHit">Enemy coordinates.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("coordinates")]
        public IHttpActionResult CheckHit([FromBody]CoordinatesDto coordinatesOfHit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =_gameService.CheckHit(coordinatesOfHit);
            if (result.IsFailure)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
            return result.Value.HasValue ? Ok(result.Value.Value) : (IHttpActionResult)NotFound();
        }
    }
}

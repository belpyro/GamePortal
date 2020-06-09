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
        private readonly GameService _gameService;

        public BattleshipGameController(GameService gameService)
        {
            this._gameService = gameService;
        }

        /// <summary>
        /// Set your own fleet coordinates on logic layer.
        /// </summary>
        /// <param name="fleetCoordinates">Own fleet coordinates.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("fleets")]
        public IHttpActionResult SetFleet([FromBody]FleetDto fleetCoordinates)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _gameService.SetFleet(fleetCoordinates);
            return result.IsSuccess ? Created($"api/battleship/game/fleets{result.Value.FleetId}", result.Value) : (IHttpActionResult)BadRequest(result.Error);
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
            return result.IsSuccess ? Ok() : (IHttpActionResult)StatusCode(HttpStatusCode.NoContent);
        }
    }
}

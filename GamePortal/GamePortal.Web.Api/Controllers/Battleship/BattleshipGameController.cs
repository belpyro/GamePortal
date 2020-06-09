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
        /// Checking hit by enemy coordinates on logic layer.
        /// </summary>
        /// <param name="fleetCoordinates">Enemy coordinates.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("coordinates")]
        public IHttpActionResult CheckHit([FromUri]CoordinatesCheck fleetCoordinates)
        {
            return Ok(_gameService.CheckHit(fleetCoordinates));
        }

        /// <summary>
        /// Set your own fleet coordinates on logic layer.
        /// </summary>
        /// <param name="fleetCoordinates">Own fleet coordinates.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult SetFleet([FromBody]FleetDto fleetCoordinates)
        {
            var result = _gameService.SetFleet(fleetCoordinates);
            return result.IsSuccess ? Created("sds", fleetCoordinates) : (IHttpActionResult)BadRequest(result.Error);
        }
    }
}

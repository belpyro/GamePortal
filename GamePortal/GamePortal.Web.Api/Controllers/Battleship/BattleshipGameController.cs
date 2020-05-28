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
        
        [HttpPost]
        [Route("point")]
        public IHttpActionResult Fire([FromUri]Point point)
        {
            return Ok(_gameService.HitCheck(point));
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PlaceFleet([FromBody]IEnumerable<Point> fleet)
        {
            _gameService.SetFleet(fleet);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}

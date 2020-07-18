using AliaksNad.Battleship.Logic.Models;
using AliaksNad.Battleship.Logic.Models.Game;
using AliaksNad.Battleship.Logic.Services;
using AliaksNad.Battleship.Logic.Services.Contracts;
using FluentValidation;
using FluentValidation.WebApi;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace GamePortal.Web.Api.Controllers.Battleship
{
    [RoutePrefix("api/battleship/game")]
    public class BattleshipGameController : ApiController
    {
        private readonly IGameService _gameService;

        public BattleshipGameController([NotNull] IGameService gameService)
        {
            this._gameService = gameService;
        }

        /// <summary>
        /// Set your own fleet coordinates on logic layer.
        /// </summary>
        /// <param name="BattleAreaDtoCoordinates">Own fleet coordinates.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> AddAsync([CustomizeValidator(RuleSet = "PreValidation")][FromBody]BattleAreaDto BattleAreaDtoCoordinates)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _gameService.AddAsync(BattleAreaDtoCoordinates);
            return result.IsSuccess ? Created($"api/battleship/game/fleets{result.Value}", result.Value) : (IHttpActionResult)BadRequest(result.Error);
        }

        /// <summary>
        /// Get all battle area from logic layer.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var result = await _gameService.GetAllAsync();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Get battle area from logic layer by id.
        /// </summary>
        /// <param name="id">battle area id.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int:min(1)}")]      // TODO: Check Route Constraints 
        public async Task<IHttpActionResult> GetByIdAsync(int id)
        {
            var battleArea = await _gameService.GetByIdAsync(id);
            if (battleArea.IsFailure)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
            return battleArea.Value.HasNoValue ? (IHttpActionResult)NotFound() : Ok(battleArea.Value.Value);
        }

        /// <summary>
        /// Checking hit by enemy coordinates on logic layer.
        /// </summary>
        /// <param name="target">Enemy coordinates.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("coordinates")]
        public async Task<IHttpActionResult> CheckHitAsync([FromBody]TargetDto target)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _gameService.CheckTargetAsync(target);
            if (result.IsFailure)
                return StatusCode(HttpStatusCode.InternalServerError);

            return result.Value.HasValue ? Ok() : (IHttpActionResult)NotFound();
        }
    }
}

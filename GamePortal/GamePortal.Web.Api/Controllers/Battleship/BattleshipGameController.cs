using AliaksNad.Battleship.Logic.Models.Game;
using AliaksNad.Battleship.Logic.Services.Contracts;
using CSharpFunctionalExtensions;
using FluentValidation.WebApi;
using GamePortal.Web.Api.Filters.Battleship;
using GamePortal.Web.Api.Hubs;
using JetBrains.Annotations;
using Microsoft.AspNet.SignalR;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace GamePortal.Web.Api.Controllers.Battleship
{
    [RoutePrefix("api/battleship/game"), ModelStateValidation
        , System.Web.Http.Authorize
        ]
    [SwaggerResponse(HttpStatusCode.Unauthorized)]
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
        [HttpPost, Route("")]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(BattleAreaDto))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(IEnumerable<Exception>))]
        public async Task<IHttpActionResult> AddAsync([CustomizeValidator(RuleSet = "PreValidation"), FromBody]BattleAreaDto BattleAreaDtoCoordinates)
        {
            var result = await _gameService.AddAsync(BattleAreaDtoCoordinates);

            if (result.IsSuccess)
            {
                var ctx = GlobalHost.ConnectionManager.GetHubContext<GameHub>();
                ctx.Clients.All.GameStart(BattleAreaDtoCoordinates.AreaId);
                return Created($"api/battleship/game/fleets{result.Value.AreaId}", result.Value);
            }

            return BadRequest(result.Error);
        }

        /// <summary>
        /// Get all battle area from logic layer.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<BattleAreaDto>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
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
        [HttpGet, Route("{id:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(BattleAreaDto))]
        public async Task<IHttpActionResult> GetByIdAsync(int id)
        {
            var battleArea = await _gameService.GetByIdAsync(id);
            if (battleArea.IsFailure)
                return StatusCode(HttpStatusCode.InternalServerError);

            return battleArea.Value.HasNoValue ? (IHttpActionResult)NotFound() : Ok(battleArea.Value.Value);
        }

        /// <summary>
        /// Checking hit by enemy coordinates on logic layer.
        /// </summary>
        /// <param name="target">Enemy coordinates.</param>
        /// <returns></returns>
        [HttpPost, Route("launch")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<IHttpActionResult> CheckHitAsync([CustomizeValidator(RuleSet = "PreValidation")][FromBody]TargetDto target)
        {
            var result = await _gameService.CheckTargetAsync(target);
            if (result.IsFailure)
                return StatusCode(HttpStatusCode.InternalServerError);

            return result.Value.HasValue ? Ok(true) : Ok(false);
        }

        /// <summary>
        /// Delete battle area by id.
        /// </summary>
        /// <param name="id">Battle area id.</param>
        /// <returns></returns>
        [HttpDelete, Route("{id:int:min(1)}")]      
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> DeleteByIdAsync(int id)
        {
            var result = await _gameService.DeleteBattleAreaAsync(id);

            return result.IsSuccess ? StatusCode(HttpStatusCode.NoContent) : StatusCode(HttpStatusCode.InternalServerError);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}

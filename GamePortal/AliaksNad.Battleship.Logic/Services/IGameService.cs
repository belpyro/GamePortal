using AliaksNad.Battleship.Logic.Models;
using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace AliaksNad.Battleship.Logic.Services
{
    public interface IGameService
    {
        /// <summary>
        /// Set your own fleet coordinates.
        /// </summary>
        /// <param name="fleetCoordinates">Own fleet coordinates.</param>
        Result<BattleAreaDto> SetFleet(BattleAreaDto fleetCoordinates);

        /// <summary>
        /// Get all battle area. 
        /// </summary>
        Result<IEnumerable<BattleAreaDto>> GetAll();

        /// <summary>
        /// Get battle area by id.
        /// </summary>
        /// <param name="id">user id.</param>
        Result<Maybe<BattleAreaDto>> GetById(int id);

        /// <summary>
        /// Checking hit by enemy coordinates.
        /// </summary>
        /// <param name="coordinates">Enemy coordinates.</param>
        /// <returns></returns>
        Result<Maybe<CoordinatesDto>> CheckHit(CoordinatesDto coordinates);
    }
}

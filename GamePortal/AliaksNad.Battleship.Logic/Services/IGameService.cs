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
        /// Checking hit by enemy coordinates.
        /// </summary>
        /// <param name="coordinates">Enemy coordinates.</param>
        /// <returns></returns>
        Result CheckHit(CoordinatesDto coordinates);
    }
}

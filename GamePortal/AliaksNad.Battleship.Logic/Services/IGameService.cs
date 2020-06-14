using AliaksNad.Battleship.Logic.Models;
using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace AliaksNad.Battleship.Logic.Services
{
    interface IGameService
    {
        /// <summary>
        /// Set your own fleet coordinates.
        /// </summary>
        /// <param name="fleetCoordinates">Own fleet coordinates.</param>
        void SetFleet(IEnumerable<Coordinates> fleetCoordinates);

        /// <summary>
        /// Checking hit by enemy coordinates.
        /// </summary>
        /// <param name="coordinates">Enemy coordinates.</param>
        /// <returns></returns>
        bool CheckHit(Coordinates coordinates);
    }
}

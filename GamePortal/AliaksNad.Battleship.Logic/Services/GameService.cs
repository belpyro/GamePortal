using AliaksNad.Battleship.Logic.Models;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AliaksNad.Battleship.Logic.Services
{
    public class GameService : IGameService 
    {
        private static IEnumerable<Coordinates> _enemyFleet;

        /// <summary>
        /// Set your own fleet coordinates on logic layer.
        /// </summary>
        /// <param name="fleetCoordinates">Own fleet coordinates.</param>
        public void SetFleet(IEnumerable<Coordinates> fleetCoordinates)
        {
            _enemyFleet = fleetCoordinates;
        }

        /// <summary>
        /// Checking hit by enemy coordinates on logic layer.
        /// </summary>
        /// <param name="fleetCoordinates">Enemy coordinates.</param>
        /// <returns></returns>
        public bool CheckHit(Coordinates fleetCoordinates)
        {
            if (_enemyFleet == null)
                throw new NullReferenceException();
            
            foreach (var item in _enemyFleet)
            {
                if (item.Equals(fleetCoordinates))
                    return true;
            }

            return false;
        }
    }
}

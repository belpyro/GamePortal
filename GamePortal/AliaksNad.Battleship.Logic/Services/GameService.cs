using AliaksNad.Battleship.Logic.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AliaksNad.Battleship.Logic.Services
{
    public class GameService : IGameService 
    {
        private static IEnumerable<Point> _enemyFleet;

        /// <summary>
        /// Fleet transfer to logic.
        /// </summary>
        /// <param name="points">Fleet.</param>
        public void SetFleet(IEnumerable<Point> points)
        {
            _enemyFleet = points;
        }

        /// <summary>
        /// Checks location for hit.
        /// </summary>
        /// <param name="point">Location.</param>
        /// <returns></returns>
        public bool HitCheck(Point point)
        {
            if (_enemyFleet == null)
                throw new NullReferenceException();
            
            foreach (var item in _enemyFleet)
            {
                if (item.Equals(point))
                    return true;
            }

            return false;
        }
    }
}

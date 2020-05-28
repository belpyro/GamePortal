using AliaksNad.Battleship.Logic.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AliaksNad.Battleship.Logic.Services
{
    public class GameService : IGameService 
    {
        private static IEnumerable<Point> _enemyFleet;

        public void SetFleet(IEnumerable<Point> points)
        {
            _enemyFleet = points;
        }

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

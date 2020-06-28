using System.Collections.Generic;

namespace AliaksNad.Battleship.Logic.Models
{
    public class BattleAreaDto
    {
        public int BattleAreaId { get; set; }

        public IEnumerable<ShipDto> Ships { get; set; }

        public IEnumerable<CoordinatesDto> FailedLaunches { get; set; }
    }
}
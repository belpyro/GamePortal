using System.Collections.Generic;

namespace AliaksNad.Battleship.Logic.Models
{
    public class BattleAreaDto
    {
        public int BattleAreaId { get; set; }

        public IEnumerable<CoordinatesDto> Ships { get; set; }

        public IEnumerable<CoordinatesDto> FailedLaunch { get; set; }
    }
}
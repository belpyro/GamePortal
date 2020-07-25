using System.Collections.Generic;

namespace AliaksNad.Battleship.Logic.Models.Game
{
    public class ShipDto
    {
        public IEnumerable<CoordinatesDto> Coordinates { get; set; }
    }
}

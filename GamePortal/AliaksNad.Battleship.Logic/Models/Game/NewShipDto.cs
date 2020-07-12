using System.Collections.Generic;

namespace AliaksNad.Battleship.Logic.Models.Game
{
    public class NewShipDto
    {
        public IEnumerable<NewCoordinatesDto> Coordinates { get; set; }
    }
}

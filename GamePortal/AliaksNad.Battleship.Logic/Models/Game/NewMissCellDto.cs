using System.Collections.Generic;

namespace AliaksNad.Battleship.Logic.Models.Game
{
    public class NewMissCellDto
    {
        public IEnumerable<NewCoordinatesDto> Coordinates { get; set; }
    }
}

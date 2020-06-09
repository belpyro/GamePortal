using System.Collections.Generic;

namespace AliaksNad.Battleship.Logic.Models
{
    public class FleetDto
    {
        public int FleetId { get; set; }

        public string Name { get; set; }

        public IEnumerable<CoordinatesDto> Coordinates { get; set; }
    }
}
using System.Collections.Generic;

namespace AliaksNad.Battleship.Data.Models
{
    public class FleetDb
    {
        public int FleetId { get; set; }

        public string Name { get; set; } = "Yes";

        public ICollection<CoordinatesDb> Coordinates { get; set; }
    }
}
using System.Collections.Generic;

namespace AliaksNad.Battleship.Data.Models
{
    public class FleetDb : FleetEntity
    {
        public ICollection<CoordinatesDb> Coordinates { get; set; }
    }
}
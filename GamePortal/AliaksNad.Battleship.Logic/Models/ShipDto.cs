using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.Models
{
    public class ShipDto
    {
        public int Id { get; set; }

        public int BattleAreaId { get; set; }

        public IEnumerable<CoordinatesDto> ShipCoordinates { get; set; }
    }
}

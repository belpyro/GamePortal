using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.Models
{
    class ShipDto
    {
        public int BattleAreaId { get; set; }

        public IEnumerable<CoordinatesDto> Coordinates { get; set; }
    }
}

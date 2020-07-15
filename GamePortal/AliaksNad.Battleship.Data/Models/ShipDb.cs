using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Data.Models
{
    public class ShipDb
    {
        public int ShipId { get; set; }

        public bool isAlife { get; set; } = true;

        public int? BattleAreaId { get; set; }

        public BattleAreaDb BattleAreas { get; set; }

        public ICollection<CoordinatesDb> Coordinates { get; set; }
    }
}

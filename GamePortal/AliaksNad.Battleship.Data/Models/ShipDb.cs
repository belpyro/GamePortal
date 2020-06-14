using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Data.Models
{
    public class ShipDb
    {
        public int Id { get; set; }

        public BattleAreaDb BattleArea { get; set; }

        public int? BattleAreaId { get; set; }

        public ICollection<CoordinatesDb> Ship { get; set; }
    }
}

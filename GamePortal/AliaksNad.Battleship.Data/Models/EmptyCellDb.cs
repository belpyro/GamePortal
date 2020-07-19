using System.Collections.Generic;

namespace AliaksNad.Battleship.Data.Models
{
    public class EmptyCellDb
    {
        public int EmptyCellId { get; set; }

        public int? BattleAreaId { get; set; }

        public BattleAreaDb BattleAreas { get; set; }

        public ICollection<CoordinatesDb> Coordinates { get; set; }
    }
}

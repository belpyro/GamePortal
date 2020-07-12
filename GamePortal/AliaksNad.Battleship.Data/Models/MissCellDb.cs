using System.Collections.Generic;

namespace AliaksNad.Battleship.Data.Models
{
    public class MissCellDb
    {
        public int MissCellId { get; set; }

        public int? BattleAreaId { get; set; }

        public BattleAreaDb BattleAreas { get; set; }

        public ICollection<CoordinatesDb> Coordinates { get; set; }
    }
}

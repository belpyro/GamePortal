using System.Collections.Generic;

namespace AliaksNad.Battleship.Data.Models
{
    public class BattleAreaDb : BattleAreaEntity
    {
        public ICollection<ShipDb> Ships { get; set; }

        public ICollection<EmptyCellDb> EmptyCells { get; set; }
    }
}
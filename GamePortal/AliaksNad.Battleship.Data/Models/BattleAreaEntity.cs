using System;

namespace AliaksNad.Battleship.Data.Models
{
    public class BattleAreaEntity
    {
        public int BattleAreaId { get; set; }

        public int CreatorId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
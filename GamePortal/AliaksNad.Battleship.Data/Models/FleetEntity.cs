using System;

namespace AliaksNad.Battleship.Data.Models
{
    public class FleetEntity
    {
        public int FleetId { get; set; }

        public int CreatorId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace AliaksNad.Battleship.Data.Models
{
    public class UserDb : UserEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<StatisticDb> Statistics { get; set; }
    }

    public class FleetDb
    {
        public int FleetId { get; set; }

        public string Name { get; set; } = "Yes";

        public ICollection<CoordinatesDb> Coordinates { get; set; }
    }

    public class CoordinatesDb
    {
        public int Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public FleetDb Fleet { get; set; }

        public int FleetId { get; set; }
    }
}
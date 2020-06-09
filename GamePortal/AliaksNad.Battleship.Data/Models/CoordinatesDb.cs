namespace AliaksNad.Battleship.Data.Models
{
    public class CoordinatesDb
    {
        public int Id { get; set; }

        public bool IsDamaged { get; set; } = false;

        public int CoordinateX { get; set; }

        public int CoordinateY { get; set; }

        public FleetDb Fleet { get; set; }

        public int? FleetId { get; set; }
    }
}
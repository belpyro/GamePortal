namespace AliaksNad.Battleship.Data.Models
{
    public class CoordinatesDb
    {
        public int Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public FleetDb Fleet { get; set; }

        public int FleetId { get; set; }
    }
}
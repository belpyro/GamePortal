namespace AliaksNad.Battleship.Data.Models
{
    public class CoordinatesDb
    {
        public int CoordinatesId { get; set; }

        public int CoordinateX { get; set; }

        public int CoordinateY { get; set; }

        public bool IsDamage { get; set; }

        public int? ShipId { get; set; }

        public ShipDb ShipDb { get; set; }

        public int? MissCellId { get; set; }

        public MissCellDb MissCellDb { get; set; }
    }
}
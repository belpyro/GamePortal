namespace AliaksNad.Battleship.Logic.Models
{
    public class CoordinatesDto
    {
        public int FleetId { get; set; }

        public bool IsDamaged { get; set; } = false;

        public int CoordinateX { get; set; }

        public int CoordinateY { get; set; }
    }
}
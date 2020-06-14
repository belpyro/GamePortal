namespace AliaksNad.Battleship.Logic.Models
{
    public class CoordinatesDto
    {
        public int Id { get; set; }

        public bool IsDamaged { get; set; }

        public int CoordinateX { get; set; }

        public int CoordinateY { get; set; }

        public int BattleAreaId { get; set; }

        public int ShipId { get; set; }
    }
}
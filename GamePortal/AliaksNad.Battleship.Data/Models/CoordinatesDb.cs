namespace AliaksNad.Battleship.Data.Models
{
    public class CoordinatesDb
    {
        public int Id { get; set; }

        public bool IsDamaged { get; set; } = false;

        public int CoordinateX { get; set; }

        public int CoordinateY { get; set; }

        public BattleAreaDb BattleArea { get; set; }

        public int? BattleAreaId { get; set; }

        public ShipDb Ships { get; set; }

        public int ShipsId { get; set; }
    }
}
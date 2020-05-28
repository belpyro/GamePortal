namespace AliaksNad.Battleship.Logic.Models
{
    public struct Point
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Point))
                return false;

            Point other = (Point)obj;
            return X == other.X && Y == other.Y;
        }

        public bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}

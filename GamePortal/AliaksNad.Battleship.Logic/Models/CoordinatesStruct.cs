namespace AliaksNad.Battleship.Logic.Models
{
    //// VS19 recommends to override the method Object.GetHashCode()
    //
    //Severity	Code	Description	Project	File	Line	Suppression State
    //Warning CS0659  'Point' overrides Object.Equals(object o) but does not override Object.GetHashCode() AliaksNad.Battleship.Logic
    //
    //// does it make sense?
    public struct CoordinatesStruct
    {
        public int X { get; set; }

        public int Y { get; set; }

        public CoordinatesStruct(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Equals check with one unboxing.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is CoordinatesStruct))
                return false;

            CoordinatesStruct other = (CoordinatesStruct)obj;
            return X == other.X && Y == other.Y;
        }

        /// <summary>
        /// Equals check without unboxing.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CoordinatesStruct other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}

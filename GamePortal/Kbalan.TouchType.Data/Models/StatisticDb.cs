namespace Kbalan.TouchType.Data.Models
{
    public class StatisticDb
    {
        public int StatisticId { get; set; }

        public int MaxSpeedRecord { get; set; } 

        public int NumberOfGamesPlayed { get; set; }

        public int AvarageSpeed { get; set; }

        virtual public UserDb User { get; set; }
    }
}

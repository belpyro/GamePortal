namespace Kbalan.TouchType.Data.Models
{
    /// <summary>
    /// Model for Statistic Table. Statistic table has PK - StatisticId and FK - User(one-to-one with UserDb). Each User has one Statistic Set and each Statistic Set
    /// has one user whom this setting set belong to.
    /// </summary>
    public class StatisticDb
    {
        public int StatisticId { get; set; }

        public int MaxSpeedRecord { get; set; } 

        public int NumberOfGamesPlayed { get; set; }

        public int AvarageSpeed { get; set; }

        public UserDb User { get; set; }
    }
}

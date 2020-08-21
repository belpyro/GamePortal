namespace Kbalan.TouchType.Logic.Dto
{
    /// <summary>
    /// Model for transfering Statistic Set from StatisticDb without related User
    /// </summary>
    public class StatisticDto
    {
        public string StatisticId { get; set; }

        public int MaxSpeedRecord { get; set; }

        public int NumberOfGamesPlayed { get; set; }

        public double AvarageSpeed { get; set; }

    }
}

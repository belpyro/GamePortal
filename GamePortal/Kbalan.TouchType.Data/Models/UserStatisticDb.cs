namespace Kbalan.TouchType.Data.Models
{
    public class UserStatisticDb
    {
        public int Id { get; set; }

        public UserDb User { get; set; }

        public int MaxSpeedRecord { get; set; } = 0;

        public int NumberOfGamesPlayed { get; set; } = 0;

        public int AvarageSpeed { get; set; } = 0;
    }
}

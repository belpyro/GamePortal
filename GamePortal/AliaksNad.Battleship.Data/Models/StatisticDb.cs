using System;

namespace AliaksNad.Battleship.Data.Models
{
    public class StatisticDb
    {
        public int Id { get; set; }

        public int Score { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
using AliaksNad.Battleship.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.DB
{
    internal class StatisticDB
    {
        internal static List<StatisticDTO> _statistics = new List<StatisticDTO>
        {
            new StatisticDTO{Id = 1, Date = new DateTime(2020, 04, 04, 16, 47, 12), Score = 10},
            new StatisticDTO{Id = 2, Date = new DateTime(2020, 05, 04, 16, 46, 12), Score = 15},
            new StatisticDTO{Id = 1, Date = new DateTime(2020, 06, 04, 16, 45, 12), Score = 12},
            new StatisticDTO{Id = 3, Date = new DateTime(2020, 07, 04, 16, 44, 12), Score = 16},
            new StatisticDTO{Id = 3, Date = new DateTime(2020, 08, 04, 16, 43, 12), Score = 11}

        };
    }
}

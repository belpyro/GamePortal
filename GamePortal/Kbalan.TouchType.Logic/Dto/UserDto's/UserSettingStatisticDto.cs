using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Dto
{
    /// <summary>
    /// Model for transfering User information with related statistic and setting
    /// </summary>
    public class UserSettingStatisticDto
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public StatisticDto Statistic { get; set; }

        public SettingDto Setting { get; set; }

    }
}

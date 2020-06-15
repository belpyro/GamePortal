﻿using System;
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
        public int Id { get; set; }

        public string NickName { get; set; }

        public string Password { get; set; }

        public StatisticDto Statistic { get; set; }

        public SettingDto Setting { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Dto.UserDto_s
{
    /// <summary>
    /// Class for info about user with statistic(without settings)
    /// </summary>
    class UserStatisticDto
    {
        public int Id { get; set; }

        public string NickName { get; set; }

        public string Password { get; set; }

        public StatisticDto Statistic { get; set; }
    }
}

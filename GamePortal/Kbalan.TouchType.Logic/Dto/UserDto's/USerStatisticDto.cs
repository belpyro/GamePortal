using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Dto
{
    /// <summary>
    ///  Model for transfering User information with related statistic, but without related setting
    /// </summary>
    public class UserStatisticDto
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public StatisticDto Statistic { get; set; }
    }
}

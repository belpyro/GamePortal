using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string NickName { get; set; }

        public UserStatisticDto Statistic { get; set; }

        public UserSettingDto Settings { get; set; }

    }
}

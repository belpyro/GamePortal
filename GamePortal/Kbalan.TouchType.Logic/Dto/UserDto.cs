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

        public UserStatisticDto statistic { get; set; }

        public UserSettingDto settings { get; set; }

    }
}

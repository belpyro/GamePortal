using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Dto
{
    /// <summary>
    ///  Model for transfering User information with related setting, but without related statistic
    /// </summary>
    public class UserSettingDto
    {
        public int Id { get; set; }

        public string NickName { get; set; }

        public string Password { get; set; }

        public SettingDto Setting { get; set; }
    }
}

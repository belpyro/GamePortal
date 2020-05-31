using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Dto
{
    public class UserAddDto
    {
        public int Id { get; set; }

        public string NickName { get; set; }

        public string Password { get; set; }

        public SettingDto Setting { get; set; }
    }
}

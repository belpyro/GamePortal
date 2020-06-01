using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Dto
{
    /// <summary>
    /// Model for transfering User information without related statistic and setting
    /// </summary>
    public class UserDto
    {
        public int Id { get; set; }

        public string NickName { get; set; }

        public string Password { get; set; }
    }
}

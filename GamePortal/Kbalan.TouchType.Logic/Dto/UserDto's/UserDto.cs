﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Dto
{
    /// <summary>
    /// Class for info about user(id, Nickname and Password) without setting or statistic
    /// </summary>
    public class UserDto
    {
        public int Id { get; set; }

        public string NickName { get; set; }

        public string Password { get; set; }
    }
}

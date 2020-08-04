using FluentValidation.Attributes;
using Kbalan.TouchType.Logic.Validators;
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
    [Validator(typeof(UserSettingDtoValidator))]
    public class UserSettingDto
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public SettingDto Setting { get; set; }
    }
}

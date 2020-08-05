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
    /// Model for transfering User information without related statistic and setting
    /// </summary>   
    public class UserDto
    {

            public string Id { get; set; }

            public string UserName { get; set; }

            public DateTime RegistrationDate { get; set; }

            public DateTime LastLoginDate { get; set; }

            public bool IsBlocked { get; set; }



    }
}

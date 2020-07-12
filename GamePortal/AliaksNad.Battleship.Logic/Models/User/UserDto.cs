using AliaksNad.Battleship.Logic.Validators;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AliaksNad.Battleship.Logic.Models.User
{
    [Validator(typeof(UserDtoValidator))]
    public class UserDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}
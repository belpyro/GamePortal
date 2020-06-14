using AliaksNad.Battleship.Logic.Validators;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AliaksNad.Battleship.Logic.Models
{
    [Validator(typeof(UserDtoValidator))]
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public IEnumerable<StatisticDto> Statistics { get; set; }
    }
}
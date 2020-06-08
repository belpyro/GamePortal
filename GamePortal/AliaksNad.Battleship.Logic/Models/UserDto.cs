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

    public class FleetDto
    {
        public int FleetId { get; set; }

        public IEnumerable<CoordinatesDto> FleetCoordinates { get; set; }
    }

    public class CoordinatesDto
    {
        public int Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
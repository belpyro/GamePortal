using AliaksNad.Battleship.Logic.Validators;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AliaksNad.Battleship.Logic.Models
{
    [Validator(typeof(BattleAreaDtoValidator))]
    public class BattleAreaDto
    {
        public int BattleAreaId { get; set; }

        public IEnumerable<ShipDto> Ships { get; set; }

        public IEnumerable<CoordinatesDto> FailedLaunches { get; set; }
    }
}
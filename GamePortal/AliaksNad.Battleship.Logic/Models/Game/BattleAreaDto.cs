using AliaksNad.Battleship.Logic.Validators;
using AliaksNad.Battleship.Logic.Validators.Game;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AliaksNad.Battleship.Logic.Models.Game
{
    [Validator(typeof(CoordinatesDtoValidator))]
    public class BattleAreaDto
    {
        public int AreaId { get; set; }

        public IEnumerable<ShipDto> Ships { get; set; }

        public IEnumerable<EmptyCellDto> EmptyCells { get; set; }
    }
}
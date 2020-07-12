using AliaksNad.Battleship.Logic.Validators;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AliaksNad.Battleship.Logic.Models.Game
{
    [Validator(typeof(BattleAreaDtoValidator))]
    public class NewBattleAreaDto
    {
        public IEnumerable<NewShipDto> Ships { get; set; }

        public IEnumerable<NewMissCellDto> MissCells { get; set; }
    }
}
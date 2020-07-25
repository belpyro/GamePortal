using AliaksNad.Battleship.Logic.Validators.Game;
using FluentValidation.Attributes;

namespace AliaksNad.Battleship.Logic.Models.Game
{
    [Validator(typeof(TargetDtoValidator))]
    public class TargetDto
    {
        public int EnemyBattleAreaId { get; set; }

        public CoordinatesDto Coordinates { get; set; }
    }
}
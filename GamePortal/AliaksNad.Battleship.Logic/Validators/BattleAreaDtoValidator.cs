using AliaksNad.Battleship.Logic.Models;
using FluentValidation;

namespace AliaksNad.Battleship.Logic.Validators
{
    class BattleAreaDtoValidator : AbstractValidator<NewBattleAreaDto>
    {
        public BattleAreaDtoValidator()
        {
            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.Ships).NotNull()
                    .WithMessage("Battle area must have ships.");
            });
        }
    }
}

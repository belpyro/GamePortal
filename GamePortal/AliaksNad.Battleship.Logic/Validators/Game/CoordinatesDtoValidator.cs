using AliaksNad.Battleship.Logic.Models;
using AliaksNad.Battleship.Logic.Models.Game;
using FluentValidation;

namespace AliaksNad.Battleship.Logic.Validators.Game
{
    public class CoordinatesDtoValidator : AbstractValidator<CoordinatesDto>
    {
        public CoordinatesDtoValidator()
        {
            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.CoordinateX).NotNull().InclusiveBetween(0, 9)
                    .WithMessage("Coordinate 'X' must be in range from 0 to 9.");
                RuleFor(x => x.CoordinateY).NotNull().InclusiveBetween(0, 9)
                    .WithMessage("Coordinate 'Y' must be in range from 0 to 9.");
            });
        }
    }
}

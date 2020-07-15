using AliaksNad.Battleship.Logic.Models;
using AliaksNad.Battleship.Logic.Models.Game;
using FluentValidation;

namespace AliaksNad.Battleship.Logic.Validators
{
    class CoordinatesDtoValidator : AbstractValidator<CoordinatesDto>
    {
        public CoordinatesDtoValidator()
        {
            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.CoordinateX).NotNull().InclusiveBetween(1, 10)
                    .WithMessage("Coordinate 'X' must be in range from 1 to 10.");
                RuleFor(x => x.CoordinateY).NotNull().InclusiveBetween(1, 10)
                    .WithMessage("Coordinate 'Y' must be in range from 1 to 10.");
            });

            RuleSet("PostValidation", () =>
            {
            });
        }
    }
}

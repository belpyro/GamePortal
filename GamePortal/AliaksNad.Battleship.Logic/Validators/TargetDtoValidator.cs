using AliaksNad.Battleship.Data.Contexts;
using AliaksNad.Battleship.Logic.Models.Game;
using FluentValidation;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.Validators
{
    class TargetDtoValidator : AbstractValidator<TargetDto>
    {
        private readonly BattleAreaContext _context;
        private readonly CoordinatesDtoValidator _validator;

        public TargetDtoValidator(BattleAreaContext context, CoordinatesDtoValidator validator)
        {
            _context = context;
            this._validator = validator;

            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.EnemyBattleAreaId).GreaterThan(0)
                .WithMessage("Wrong enemy id.");
                RuleFor(x => x.Coordinates).SetValidator(_validator);
            });

            RuleSet("PostValidation", () =>
            {
                RuleFor(x => x).MustAsync((x, cancellation) => CheckDuplicate(x))
                .WithMessage("This Coordinates already exist.");
            });
        }

        private async Task<bool> CheckDuplicate(TargetDto target)
        {
            var result = await _context.Coordinates.AsNoTracking().Where(x => x.CoordinatesId == target.EnemyBattleAreaId)
                .Where(x => x.CoordinateX == x.CoordinateX && x.CoordinateY == target.Coordinates.CoordinateY).ToArrayAsync();

            if (result != null)
            {
                return false;
            }
            return true;
        }
    }
}

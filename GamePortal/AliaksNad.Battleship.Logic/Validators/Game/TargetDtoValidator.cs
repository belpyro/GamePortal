using AliaksNad.Battleship.Data.Contexts;
using AliaksNad.Battleship.Logic.Models.Game;
using FluentValidation;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.Validators.Game
{
    public class TargetDtoValidator : AbstractValidator<TargetDto>
    {
        private readonly BattleAreaContext _context;
        private readonly CoordinatesDtoValidator _validator;

        public TargetDtoValidator(BattleAreaContext context, CoordinatesDtoValidator validator)
        {
            _context = context;
            _validator = validator;

            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.EnemyBattleAreaId).GreaterThan(0)
                .WithMessage("Wrong enemy id.");
                RuleFor(x => x.Coordinates).SetValidator(_validator);
            });

            RuleSet("PostValidation", () =>
            {
                RuleFor(x => x.EnemyBattleAreaId).GreaterThan(0)
                .WithMessage("Wrong enemy id.");

                RuleFor(x => x).Must(CheckDuplicateAsync)
                .WithMessage("This Coordinates already exist.");
            });
        }

        private bool CheckDuplicateAsync(TargetDto target)
        {
            var result = _context.EmptyCell.AsNoTracking().Where(x => x.BattleAreaId == target.EnemyBattleAreaId).SelectMany(x => x.Coordinates)
                .Where(x => x.CoordinateX == x.CoordinateX && x.CoordinateY == target.Coordinates.CoordinateY).ToArray();

            var result2 = _context.Ships.AsNoTracking().Where(x => x.BattleAreaId == target.EnemyBattleAreaId).SelectMany(x => x.Coordinates)
                .Where(x => x.CoordinateX == x.CoordinateX && x.CoordinateY == target.Coordinates.CoordinateY).ToArray();

            if (result != null && result2 != null)
            {
                return false;
            }

            return true;
        }
    }
}

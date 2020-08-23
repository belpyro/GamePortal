using FluentValidation;
using Kbalan.TouchType.Data.Contexts;
using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Validators
{
    public class StatisticDtoValidator: AbstractValidator<StatisticDto>
    {
        private readonly TouchTypeGameContext _context;

        public StatisticDtoValidator(TouchTypeGameContext context)
        {
            /* Rule Set for validarion on presentation layer. 
               Rules: 
               1. Avarage speed must be greater than zero or equal
               2. Maximum speed must be greater than zero or equal
               3. Number of games played must be greater than zero or equal*/
            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.AvarageSpeed).GreaterThanOrEqualTo(0).WithMessage("Avarage speed should be greater than zero(or equal)");
                RuleFor(x => x.MaxSpeedRecord).GreaterThanOrEqualTo(0).WithMessage("Maximum speed should be greater than zero(or equal)");
                RuleFor(x => x.NumberOfGamesPlayed).GreaterThanOrEqualTo(0).WithMessage("Number of games should be greater than zero(or equal)");
            });

            /* Rule Set for validarion on presentation layer. 
               Rules: 
               1. Number of games played must be greater that it were before
               2. Speed record must be equal or greater than before*/
            RuleSet("PostValidation", () =>
            {
                RuleFor(x => x).Must(NotLessGamesThanPrevious).WithMessage("Number of games can't be less than before");
                RuleFor(x => x).Must(NotLessRecordThanPrevious).WithMessage("Speed record can't be less than before");
            });
            this._context = context;
        }

        /// <summary>
        /// Method for comparing Number of games played in Dto with Number of games played in Db
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool NotLessGamesThanPrevious(StatisticDto arg)
        {
            return _context.Statistics.AsNoTracking().SingleOrDefault(x => x.StatisticId == arg.StatisticId).NumberOfGamesPlayed <= arg.NumberOfGamesPlayed;
        }

        /// <summary>
        /// Method for comparing speed record in Dto with Number of games played in Db
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool NotLessRecordThanPrevious(StatisticDto arg)
        {
            return _context.Statistics.AsNoTracking().SingleOrDefault(x => x.StatisticId == arg.StatisticId).MaxSpeedRecord <= arg.MaxSpeedRecord;
        }

    }
}

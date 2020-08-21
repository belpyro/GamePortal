using FluentValidation;
using Kbalan.TouchType.Data.Contexts;
using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Validators
{
    public class SettingDtoValidator: AbstractValidator<SettingDto>
    {
        private readonly TouchTypeGameContext _context;

        public SettingDtoValidator(TouchTypeGameContext context)
        {
             /* Rule Set for validarion on presentation layer. 
                Rules: 
                1. Level of Text must be matching LevelOfText enum*/
            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.LevelOfText).IsInEnum().WithMessage("Level must be Easy(0), Middle(1) or Hard(2)");
            });

        }
    }
}

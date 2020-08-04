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

            /* Rule Set for validarion on logic layer with handling to context. 
               Rules: 
               1. No user with such nickname shouldn't exist except user wich settings are changing*/
         //   RuleSet("PostValidation", () =>
         //   {
          //      RuleFor(x => x).Must(CheckDuplicateEmail).WithMessage("Such Email is already exist");
          //  });
            this._context = context;
        }

        /// <summary>
        /// Checking if user with such email is already exists in context, except current user
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
       // private bool CheckDuplicateEmail(SettingDto arg)
       // {
       //     return !_context.ApplicationUsers.AsNoTracking().Where(x => x.Setting.SettingId != arg.SettingId ).Any(x => x.Email == arg.Email);
       // }

    }
}

using FluentValidation;
using Kbalan.TouchType.Data.Contexts;
using Kbalan.TouchType.Data.Models;
using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Validators
{
    /// <summary>
    /// Class for validation of UserServiceDto
    /// </summary>
    public class UserSettingDtoValidator : AbstractValidator<UserSettingDto>
    {
        private readonly TouchTypeGameContext _context;

        public UserSettingDtoValidator(TouchTypeGameContext context)
        {
            /* Rule Set for validarion on presentation layer. 
                Rules: 
                1. Nickname must be more than three symbols. 
                2. Password must be more than five symbols
                3. Password must contain at least one uppercase symbol and one number. And shouldn't contain whitespaces.
                4. Email should have valid email format 
                5. Level of Text must be matching LevelOfText enum
                6. Role must be matching Role enum*/
            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.NickName).NotNull().MinimumLength(3)
                                .WithMessage("Name should have more than 2 symbols");
                RuleFor(x => x.Password).NotNull().MinimumLength(6)
                                .WithMessage("Password should have more than 5 symbols")
                                .Must(CheckPassword)
                                .WithMessage("Password should contain at least one uppercase symbol and number and NO whitespaces!!");
                RuleFor(x => x.Setting.Email).EmailAddress().WithMessage("Incorrect Email!!1");
                RuleFor(x => x.Setting.LevelOfText).IsInEnum().WithMessage("Level must be Easy(0), Middle(1) or Hard(2)");
                RuleFor(x => x.Setting.Role).IsInEnum().WithMessage("Role must be User(0), Premium User(1) or Admin(2)");
            });

            /* Rule Set for validarion on logic layer with handling to context. 
               Rules: 
               1. No user wiht such nickname shouldn't exist.*/
            RuleSet("PostValidation", () =>
            {
                RuleFor(x => x.NickName).Must(CheckDublicate).WithMessage("Such Nickname is already exists");
            });
            this._context = context;
        }

        /// <summary>
        /// Method for checking if password contain Uppercases, number and whitespaces
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        private bool CheckPassword(string pass)
        {
            return pass.Any(Char.IsDigit) && pass.Any(char.IsUpper) && !pass.Any(char.IsWhiteSpace);
        }

        /// <summary>
        /// Checking if user with such nickname is alreadu exists in context
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool CheckDublicate(string name)
        {
            return  !_context.Users.AsNoTracking().Any(x => x.NickName == name);
        }
    }
}

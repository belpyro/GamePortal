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
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        private readonly TouchTypeGameContext _context;

        public UserDtoValidator(TouchTypeGameContext context)
        {
            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.NickName).NotNull().MinimumLength(3)
                                .WithMessage("Name should have more than 2 symbols");
                RuleFor(x => x.Password).NotNull().MinimumLength(6)
                                .WithMessage("Password should have more than 5 symbols")
                                .Must(CheckPassword)
                                .WithMessage("Password should contain at least one uppercase symbol and number and NO whitespaces!!");
            });

            RuleSet("PostValidation", () =>
            {
                RuleFor(x => x.NickName).Must(CheckDublicate).WithMessage("Such Nickname is already exists");
            });
            this._context = context;
        }

        private bool CheckPassword(string pass)
        {
            return pass.Any(Char.IsDigit) && pass.Any(char.IsUpper) && !pass.Any(char.IsWhiteSpace);
        }

        private bool CheckDublicate(string name)
        {
            return !_context.Users.AsNoTracking().Any(x => x.NickName == name);
        }
    }

}

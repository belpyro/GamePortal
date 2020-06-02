using AliaksNad.Battleship.Data.Contexts;
using AliaksNad.Battleship.Logic.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.Validators
{
    class UserDtoValidator : AbstractValidator<UserDto>
    {
        private readonly UsersContexts _contexts;

        public UserDtoValidator(UsersContexts contexts)
        {
            this._contexts = contexts;

            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.Name).NotNull().MinimumLength(5) //TODO: check MinLength rule
                    .WithMessage("Field name is invalid");
            });
            
            RuleSet("PostValidation", () =>
            {
                RuleFor(x => x.Name).Must(CheckDuplicate);
            });
        }

        private bool CheckDuplicate(string name)
        {
            return !_contexts.Users.AsNoTracking().Any(x => x.Name == name);
        }
    }
}

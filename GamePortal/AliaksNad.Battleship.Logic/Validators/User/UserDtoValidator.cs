using AliaksNad.Battleship.Data.Contexts;
using AliaksNad.Battleship.Logic.Models;
using AliaksNad.Battleship.Logic.Models.User;
using FluentValidation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.Validators.User
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        private readonly BattleAreaContext _context;

        public UserDtoValidator(BattleAreaContext context)
        {
            this._context = context;

            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.UserName).NotNull().Length(3, 15) //TODO: check MinLength rule
                    .WithMessage("Field Name is invalid");
            });

            RuleSet("PostValidation", () =>
            {
                RuleFor(x => x.UserName).Must(CheckDuplicate);
            });
        }

        private bool CheckDuplicate(string name)
        {
            return !_context.Users.AsNoTracking().Any(x => x.UserName == name);
        }
    }
}

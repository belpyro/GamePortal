using AliaksNad.Battleship.Data.Contexts;
using AliaksNad.Battleship.Logic.Models;
using AliaksNad.Battleship.Logic.Models.User;
using FluentValidation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.Validators
{
    class UserDtoValidator : AbstractValidator<UserDto>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserDtoValidator(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;

            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.UserName).NotNull().Length(3, 15) //TODO: check MinLength rule
                    .WithMessage("Field Name is invalid");
            });

            //RuleSet("PostValidation", () =>
            //{
            //    RuleFor(x => x.Name).Must(CheckDuplicate);
            //});
        }

        //private bool CheckDuplicate(string name)
        //{
        //    return !_contexts.Users.AsNoTracking().Any(x => x.Name == name);
        //}
    }
}

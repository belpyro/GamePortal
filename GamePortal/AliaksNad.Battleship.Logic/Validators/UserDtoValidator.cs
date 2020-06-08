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

        //public UserDtoValidator(UsersContexts contexts)
        //{
        //    this._contexts = contexts;

        //    RuleSet("PreValidation", () =>
        //    {
        //        RuleFor(x => x.Name).NotNull().Length(3, 15) //TODO: check MinLength rule
        //            .WithMessage("Field Name is invalid");
        //        RuleFor(x => x.Email).EmailAddress()
        //            .WithMessage("Field Email is invalid");
        //        RuleFor(x => x.Password).NotNull().Length(6, 15)
        //            .WithMessage("Field Password is invalid");
        //    });
            
        //    RuleSet("PostValidation", () =>
        //    {
        //        RuleFor(x => x.Name).Must(CheckDuplicate);
        //    });
        //}

        private bool CheckDuplicate(string name)
        {
            return !_contexts.Users.AsNoTracking().Any(x => x.Name == name);
        }
    }
}

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
    /// <summary>
    /// Class for validation of UserDto
    /// </summary>
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        private readonly TouchTypeGameContext _context;

        public UserDtoValidator(TouchTypeGameContext context)
        {
             /* Rule Set for validarion on presentation layer. 
                Rules: 
                1. Id must be greater than zero. 
                2. Nickname must be more than three symbols. 
                3. Password must be more than five symbols
                4. Password must contain at least one uppercase symbol and one number. And shouldn't contain whitespaces.*/
            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
                RuleFor(x => x.NickName).MinimumLength(3)
                                .WithMessage("Name should have more than 2 symbols");
                RuleFor(x => x.Password).NotNull().MinimumLength(6)
                                .WithMessage("Password should have more than 5 symbols")
                                .Must(CheckPassword)
                                .WithMessage("Password should contain at least one uppercase symbol and number and NO whitespaces!!");
               
            });

             /* Rule Set for validarion on logic layer with handling to context. 
                Rules: 
                1. User with such Id should exist. 
                2. No user wiht such nickname shouldn't exist.*/
            RuleSet("PostValidation", () =>
            {
                RuleFor(x => x.Id).Must(CheckId).WithMessage("No user with such Id exist");
                RuleFor(x => x.NickName).Must(CheckDublicate).WithMessage("Such Nickname is already exists");
                
            });
            this._context = context;
        }

        /// <summary>
        /// Method for checking if user with such id is already exists in context
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool CheckId(int id)
        {
            return _context.Users.AsNoTracking().Any(x => x.Id == id);
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
            return !_context.Users.AsNoTracking().Any(x => x.NickName == name);
        }
    }

}

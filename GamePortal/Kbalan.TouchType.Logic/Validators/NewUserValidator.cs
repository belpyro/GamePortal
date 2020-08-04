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
    public class NewUserValidator : AbstractValidator<NewUserDto>
    {
        private readonly TouchTypeGameContext _context;

        public NewUserValidator(TouchTypeGameContext context)
        {
            /* Rule Set for validarion on presentation layer. 
                Rules: 
                1. Nickname must be more than three symbols. 
                2. Password must be more than five symbols
                3. Password must contain at least one uppercase symbol and one number. And shouldn't contain whitespaces.
                4. Email should have valid email format */
            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.UserName).NotNull().MinimumLength(3)
                                .WithMessage("Name should have more than 2 symbols");
                RuleFor(x => x.Password).NotNull().MinimumLength(6)
                                .WithMessage("Password should have more than 5 symbols")
                                .Must(CheckPassword)
                                .WithMessage("Password should contain at least one uppercase symbol and number and NO whitespaces!!");
                RuleFor(x => x.Email).EmailAddress().WithMessage("Incorrect Email!!1");
            });

            /* Rule Set for validarion on logic layer with handling to context. 
               Rules: 
               1. No user wiht such nickname shouldn't exist.*/
            RuleSet("PostValidation", () =>
            {
                RuleFor(x => x.UserName).Must(CheckDublicateName).WithMessage("Such Nickname is already exist");
                RuleFor(x => x.Email).Must(CheckDuplicateEmail).WithMessage("Such Email is already exist");
            });
            this._context = context;
        }

        /// <summary>
        /// Checking if user with such email is already exists in context
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool CheckDuplicateEmail(string email)
        {
            return !_context.ApplicationUsers.AsNoTracking().Any(x => x.Email == email);
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
        private bool CheckDublicateName(string name)
        {
            return  !_context.ApplicationUsers.AsNoTracking().Any(x => x.UserName == name);
        }
    }
}

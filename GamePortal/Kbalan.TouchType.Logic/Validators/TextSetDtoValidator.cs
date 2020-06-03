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
    public class TextSetDtoValidator: AbstractValidator<TextSetDto>
    {
        private readonly TouchTypeGameContext _context;

        public TextSetDtoValidator(TouchTypeGameContext context)
        {
            /* Rule Set for validarion on presentation layer. 
               Rules: 
               1. User with such Id should exist. 
               2. Name of text must contain more than 4 symbols
               3. Level of Text must be matching LevelOfText enum
               4. Text must contain more than 30 symbols*/
            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.Id).Must(CheckId).WithMessage("No text with such Id exist");
                RuleFor(x => x.Name).MinimumLength(5).WithMessage("Name of the text must contain more than four symbols");
                RuleFor(x => x.LevelOfText).IsInEnum().WithMessage("Level must be Easy(0), Middle(1) or Hard(2)");
                RuleFor(x => x.TextForTyping).NotEmpty().NotNull().MinimumLength(30).WithMessage("Text must contaion more than 30 symbols");
            });

            /* Rule Set for validarion on logic layer with handling to context. 
               Rules: 
               1. No user with such nickname shouldn't exist except user wich settings are changing*/
            RuleSet("PostValidation", () =>
            {
                RuleFor(x => x).Must(CheckDuplicateName).WithMessage("Such Name is already exist");
            });
            this._context = context;
        }

        /// <summary>
        /// Checking if text set with such name is alreadu exists in context
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool CheckDuplicateName(TextSetDto arg)
        {
            return !_context.TextSets.AsNoTracking().Where(x => x.Id != arg.Id).Any(x => x.Name == arg.Name);
        }

        /// <summary>
        /// Method for checking if textset with such id is already exists in context
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool CheckId(int id)
        {
            return _context.TextSets.AsNoTracking().Any(x => x.Id == id);
        }

    }
}

using FluentValidation;
using FluentValidation.Results;
using Castle.DynamicProxy;
using CSharpFunctionalExtensions;
using Kbalan.TouchType.Logic.Dto;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kbalan.TouchType.Logic.Validators;

namespace Kbalan.TouchType.Logic.Aspects
{
    public class TextSetValidationInterceptor : IInterceptor
    {
        private readonly IKernel _kernel;

        public TextSetValidationInterceptor(IKernel kernel)
        {
            this._kernel = kernel;
        }
        public void Intercept(IInvocation invocation)
        {
            var arg = invocation.Arguments.SingleOrDefault(x => x.GetType() == typeof(TextSetDto));
            if(arg == null)
            {
                invocation.Proceed();
                return;
            }

            if (invocation.Method.Name.Equals("Add"))
            {
                var validator = _kernel.Get<IValidator<TextSetDto>>();
                var validationResult = validator.Validate(arg as TextSetDto, ruleSet: "PostValidation");

                if (!validationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure<TextSetDto>(validationResult.Errors.Select(x => x.ErrorMessage).First());
                    return;
                }
            }

            if (invocation.Method.Name.Equals("Update"))
            {
                var validator = _kernel.Get<IValidator<TextSetDto>>();
                var validationResult = validator.Validate(arg as TextSetDto, ruleSet: "PostValidationWithId"); 
                
                if (!validationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure(validationResult.Errors.Select(x => x.ErrorMessage).First());
                    return;
                }
            }

            invocation.Proceed();
        }
    }
}

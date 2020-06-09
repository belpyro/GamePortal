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
using Serilog;

namespace Kbalan.TouchType.Logic.Aspects
{
    /// <summary>
    /// Interceptor for TextSetService Proxy. It includes PostValidation for Add and Update method
    /// </summary>
    public class TextSetValidationInterceptor : IInterceptor
    {
        private readonly IKernel _kernel;
        private object validationResult;

        public TextSetValidationInterceptor(IKernel kernel)
        {
            this._kernel = kernel;
        }
        public void Intercept(IInvocation invocation)
        {
            //model null checking
            var text = invocation.Arguments.OfType<TextSetDto>().FirstOrDefault();
            if(text == null)
            {
                invocation.Proceed();
                return;
            }
            //New logger and validator
            var logger = _kernel.Get<ILogger>();
            var validator = _kernel.Get<IValidator<TextSetDto>>();

            //Prevalidation
            if(invocation.Method.Name.Equals("Add") || invocation.Method.Name.Equals("Update"))
            {
                var preValidationResult = validator.Validate(text as TextSetDto, ruleSet: "PreValidation");
                if (!preValidationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure(preValidationResult.Errors.Select(x => x.ErrorMessage).First());
                    return;
                }
            }

            //PostValidation of validation for Add method 
            if (invocation.Method.Name.Equals("Add"))
            {
                var postValidationResult = validator.Validate(text as TextSetDto, ruleSet: "PostValidation");
                if (!postValidationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure<TextSetDto>(postValidationResult.Errors.Select(x => x.ErrorMessage).First());
                    return;
                }
            }

            //PostValidation validation for Update method 
            if (invocation.Method.Name.Equals("Update"))
            {
                var postValidationResult = validator.Validate(text as TextSetDto, ruleSet: "PostValidationWithId");                 
                if (!postValidationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure(postValidationResult.Errors.Select(x => x.ErrorMessage).First());
                    return;
                }
            }

            invocation.Proceed();
        }
    }
}

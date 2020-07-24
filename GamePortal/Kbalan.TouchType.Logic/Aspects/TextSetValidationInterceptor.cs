using FluentValidation;
using FluentValidation.Results;

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
using Ninject.Extensions.Interception;
using Kbalan.TouchType.Logic.Exceptions;

namespace Kbalan.TouchType.Logic.Aspects
{
    /// <summary>
    /// Interceptor for TextSetService Proxy. It includes PostValidation for Add and Update method
    /// </summary>
    public class TextSetValidationInterceptor : IInterceptor
    {
        private readonly IKernel _kernel;

        public TextSetValidationInterceptor(IKernel kernel)
        {
            this._kernel = kernel;
        }
        public  void Intercept(IInvocation invocation)
        {
           

                var text = invocation.Request.Arguments.OfType<TextSetDto>().FirstOrDefault();
                //model null checking
                if (text == null)
                {
                    invocation.Proceed();
                    return;
                }
                //New logger and validator
                var logger = _kernel.Get<ILogger>();
                var validator = _kernel.Get<IValidator<TextSetDto>>();

                //Prevalidation
                if (invocation.Request.Method.Name.Equals("AddAsync") || invocation.Request.Method.Name.Equals("UpdateAsync"))
                {
                    var preValidationResult = validator.Validate(text as TextSetDto, ruleSet: "PreValidation");
                    if (!preValidationResult.IsValid)
                    {
                        invocation.ReturnValue = Result.Failure<TextSetDto>(preValidationResult.Errors.Select(x => x.ErrorMessage).First());
                        throw new TTGValidationException(invocation.ReturnValue.ToString());
                    }
                }

                //PostValidation of validation for Add method 
                if (invocation.Request.Method.Name.Equals("AddAsync"))
                {
                    var postValidationResult = validator.Validate(text as TextSetDto, ruleSet: "PostValidation");
                    if (!postValidationResult.IsValid)
                    {
                        invocation.ReturnValue = Result.Failure<TextSetDto>(postValidationResult.Errors.Select(x => x.ErrorMessage).First());
                        throw new TTGValidationException(invocation.ReturnValue.ToString());
                    }
                }

                //PostValidation validation for Update method 
                if (invocation.Request.Method.Name.Equals("UpdateAsync"))
                {
                    var postValidationResult = validator.Validate(text as TextSetDto, ruleSet: "PostValidationWithId");
                    if (!postValidationResult.IsValid)
                    {
                        invocation.ReturnValue = Result.Failure(postValidationResult.Errors.Select(x => x.ErrorMessage).First());
                        throw new TTGValidationException(invocation.ReturnValue.ToString());
                    }
                }

                invocation.Proceed();


        }
    }
}


using CSharpFunctionalExtensions;
using FluentValidation;
using Kbalan.TouchType.Logic.Dto;
using Ninject;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Extensions.Interception;
using Kbalan.TouchType.Logic.Exceptions;

namespace Kbalan.TouchType.Logic.Aspects
{
    /// <summary>
    /// Interceptor for UserService Proxy. It includes PostValidation for Add and Update method
    /// </summary>
    class UserValidationInterceptor : IInterceptor
    {
        private readonly IKernel _kernel;

        public UserValidationInterceptor(IKernel kernel)
        {
            this._kernel = kernel;
        }
        public void Intercept(IInvocation invocation)
        {
            //model null checking. One of models must exist
            var user = invocation.Request.Arguments.OfType<NewUserDto>().SingleOrDefault();
            if (user == null)
            {
                invocation.Proceed();
                return;
            }
            //New logger and validator
            var logger = _kernel.Get<ILogger>();
            var userValidator = _kernel.Get<IValidator<NewUserDto>>();


            //Prevalidation for Add method
            if (invocation.Request.Method.Name.Equals("Register"))
            {
                var preValidationResult = userValidator.Validate(user as NewUserDto, ruleSet: "PreValidation");
                if (!preValidationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure<NewUserDto>(preValidationResult.Errors.Select(x => x.ErrorMessage).First());
                    throw new TTGValidationException(invocation.ReturnValue.ToString());
                }
            }

            //Prevalidation for Update method
            if (invocation.Request.Method.Name.Equals("Update"))
            {
                var preValidationResult = userValidator.Validate(user as NewUserDto, ruleSet: "PreValidation");
                if (!preValidationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure(preValidationResult.Errors.Select(x => x.ErrorMessage).First());
                    throw new TTGValidationException(invocation.ReturnValue.ToString());
                }
            }

            //Postvalidation for Add method 
            if (invocation.Request.Method.Name.Equals("Register"))
            {
                var validator = _kernel.Get<IValidator<NewUserDto>>();
                var validationResult = validator.Validate(user as NewUserDto, ruleSet: "PostValidation");

                if (!validationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure<NewUserDto>(validationResult.Errors.Select(x => x.ErrorMessage).First());
                    throw new TTGValidationException(invocation.ReturnValue.ToString());
                }
            }

            //Postvalidation for Update method 
            if (invocation.Request.Method.Name.Equals("Update"))
            {
                var validator = _kernel.Get<IValidator<NewUserDto>>();
                var validationResult = validator.Validate(user as NewUserDto, ruleSet: "PostValidation");

                if (!validationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure(validationResult.Errors.Select(x => x.ErrorMessage).First());
                    throw new TTGValidationException(invocation.ReturnValue.ToString());
                }
            }

            invocation.Proceed();
        }
    }
}

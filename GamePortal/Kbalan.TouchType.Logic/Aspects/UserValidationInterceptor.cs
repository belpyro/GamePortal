using Castle.DynamicProxy;
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
            var user = invocation.Arguments.OfType<UserDto>().SingleOrDefault();
            var userSetting = invocation.Arguments.OfType<UserSettingDto>().SingleOrDefault();
            if (user == null && userSetting == null)
            {
                invocation.Proceed();
                return;
            }
            //New logger and validator
            var logger = _kernel.Get<ILogger>();
            var userSettingValidator = _kernel.Get<IValidator<UserSettingDto>>();
            var userValidator = _kernel.Get<IValidator<UserDto>>();


            //Prevalidation for Add method
            if (invocation.Method.Name.Equals("Add"))
            {
                var preValidationResult = userSettingValidator.Validate(userSetting as UserSettingDto, ruleSet: "PreValidation");
                if (!preValidationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure<UserSettingDto>(preValidationResult.Errors.Select(x => x.ErrorMessage).First());
                    return;
                }
            }

            //Prevalidation for Update method
            if (invocation.Method.Name.Equals("Update"))
            {
                var preValidationResult = userValidator.Validate(user as UserDto, ruleSet: "PreValidation");
                if (!preValidationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure(preValidationResult.Errors.Select(x => x.ErrorMessage).First());
                    return;
                }
            }

            //Postvalidation for Add method 
            if (invocation.Method.Name.Equals("Add"))
            {
                var validator = _kernel.Get<IValidator<UserSettingDto>>();
                var validationResult = validator.Validate(userSetting as UserSettingDto, ruleSet: "PostValidation");

                if (!validationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure<UserSettingDto>(validationResult.Errors.Select(x => x.ErrorMessage).First());
                    return;
                }
            }

            //Postvalidation for Update method 
            if (invocation.Method.Name.Equals("Update"))
            {
                var validator = _kernel.Get<IValidator<UserDto>>();
                var validationResult = validator.Validate(user as UserDto, ruleSet: "PostValidation");

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

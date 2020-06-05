using Castle.DynamicProxy;
using CSharpFunctionalExtensions;
using FluentValidation;
using Kbalan.TouchType.Logic.Dto;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Aspects
{
    class UserValidationInterceptor : IInterceptor
    {
        private readonly IKernel _kernel;

        public UserValidationInterceptor(IKernel kernel)
        {
            this._kernel = kernel;
        }
        public void Intercept(IInvocation invocation)
        {
            var user = invocation.Arguments.SingleOrDefault(x => x.GetType() == typeof(UserDto));
            var userSetting = invocation.Arguments.SingleOrDefault(x => x.GetType() == typeof(UserSettingDto));
            if (user == null && userSetting == null)
            {
                invocation.Proceed();
                return;
            }

            //Implementation of validation for Add method 
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

            //Implementation of validation for Update method 
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

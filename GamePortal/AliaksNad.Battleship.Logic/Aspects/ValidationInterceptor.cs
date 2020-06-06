using AliaksNad.Battleship.Logic.Models;
using Castle.DynamicProxy;
using CSharpFunctionalExtensions;
using FluentValidation;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic.Aspects
{
    class ValidationInterceptor : IInterceptor
    {
        private readonly IKernel _kernal;

        public ValidationInterceptor(IKernel kernal)
        {
            _kernal = kernal;
        }

        public void Intercept(IInvocation invocation)
        {
            var arg = invocation.Arguments.OfType<UserDto>().FirstOrDefault();
            if (arg == null)
            {
                invocation.Proceed();
                return;
            }

            var validator = _kernal.Get<IValidator<UserDto>>();
            var validationResult = validator.Validate(arg, "PostValidation");
            if (!validationResult.IsValid)
            {
                invocation.ReturnValue = Result.Failure<UserDto>(validationResult.Errors.Select(x => x.ErrorMessage).First());
            }

            invocation.Proceed();
        }
    }
}

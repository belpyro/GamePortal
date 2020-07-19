

using AliaksNad.Battleship.Logic.Models;
using AliaksNad.Battleship.Logic.Models.Game;
using AliaksNad.Battleship.Logic.Models.User;
using CSharpFunctionalExtensions;
using FluentValidation;
using Ninject;
using Ninject.Extensions.Interception;
using System.Linq;

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
            var arg = invocation.Request.Arguments.OfType<UserDto>().FirstOrDefault();
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

    class ValidationInterceptor2 : SimpleInterceptor
    {
        private readonly IKernel _kernal;

        public ValidationInterceptor2(IKernel kernal)
        {
            _kernal = kernal;
        }

        protected override void BeforeInvoke(IInvocation invocation)
        {
            var arg = invocation.Request.Arguments.OfType<TargetDto>().FirstOrDefault();
            if (arg != null)
            {
                var validator = _kernal.Get<IValidator<TargetDto>>();
                var validationResult = validator.Validate(arg as TargetDto, "PreValidation");
                if (!validationResult.IsValid)
                {
                    invocation.ReturnValue = Result.Failure<UserDto>(validationResult.Errors.Select(x => x.ErrorMessage).First());
                }
            }
        }

        protected override void AfterInvoke(IInvocation invocation)
        {
            base.AfterInvoke(invocation);
        }
    }
}

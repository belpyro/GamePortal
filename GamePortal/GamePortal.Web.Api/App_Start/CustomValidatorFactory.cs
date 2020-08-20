using AliaksNad.Battleship.Logic.Models.Game;
using AliaksNad.Battleship.Logic.Models.User;
using FluentValidation;
using Ninject;
using System;
using System.Web.Http.Dependencies;

namespace GamePortal.Web.Api
{
    public class CustomValidatorFactory : IValidatorFactory
    {
        private IDependencyResolver _dependencyResolver;
        private IKernel kernel;

        public CustomValidatorFactory(IDependencyResolver dependencyResolver)
        {
            this._dependencyResolver = dependencyResolver;
        }

        public CustomValidatorFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public IValidator<T> GetValidator<T>()
        {
            return kernel.TryGet(typeof(T)) as IValidator<T>;
        }

        public IValidator GetValidator(Type type)
        {
            var result = typeof(new Validator<type>);
            var resultOld = GetKernelValidator(type);

            if (result == null)
            {
                return kernel.TryGet(type) as IValidator;
            }

            return result;
        }

        private IValidator GetKernelValidator(Type type)
        {
            if (type.Name == typeof(TargetDto).Name)
            {
                return kernel.TryGet<IValidator<TargetDto>>();
            }
            if (type.Name == typeof(BattleAreaDto).Name)
            {
                return kernel.TryGet<IValidator<BattleAreaDto>>();
            }
            if (type.Name == typeof(NewUserDto).Name)
            {
                return kernel.TryGet<IValidator<NewUserDto>>();
            }
            if (type.Name == typeof(LoginDto).Name)
            {
                return kernel.TryGet<IValidator<LoginDto>>();
            }

            return null;
        }
    }
}

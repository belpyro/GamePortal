using AliaksNad.Battleship.Data.Contexts;
using AliaksNad.Battleship.Logic.Models;
using AliaksNad.Battleship.Logic.Profiles;
using AliaksNad.Battleship.Logic.Services;
using AliaksNad.Battleship.Logic.Validators;
using AutoMapper;
using FluentValidation;
using Ninject.Modules;
using Castle.DynamicProxy;
using Ninject;
using AliaksNad.Battleship.Logic.Aspects;
using Serilog;
using Ninject.Extensions.Interception.Infrastructure.Language;

namespace AliaksNad.Battleship.Logic
{
    public class BattleshipLogicDIModule : NinjectModule
    {
        public override void Load()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(typeof(UserProfile)));
            var mapper = configuration.CreateMapper();

            this.Bind<IMapper>().ToConstant(mapper)
                .When(r =>
                {
                    return r.ParentContext != null && r.ParentContext.Plan.Type.Namespace.StartsWith("AliaksNad.Battleship");
                });

            var logger = new LoggerConfiguration().CreateLogger();

            this.Bind<ILogger>().ToConstant(logger)
                .When(r =>
                {
                    return r.ParentContext != null && r.ParentContext.Plan.Type.Namespace.StartsWith("AliaksNad.Battleship");
                });

            this.Bind<UsersContexts>().ToSelf();
            this.Bind<IValidator<UserDto>>().To<UserDtoValidator>();

            this.Bind<IUserService>().To<UserService>().Intercept().With<ValidationInterceptor>();
            //    .ToMethod(ctx =>
            //{
            //    var service = new UserService(ctx.Kernel.Get<UsersContexts>(), ctx.Kernel.Get<IMapper>(), ctx.Kernel.Get<ILogger>());
            //    return new ProxyGenerator().CreateInterfaceProxyWithTarget<IUserService>(service, new ValidationInterceptor(ctx.Kernel));
            //});
            this.Bind<IGameService>().To<GameService>();
        }
    }
}

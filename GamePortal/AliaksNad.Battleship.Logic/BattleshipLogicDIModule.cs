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

namespace AliaksNad.Battleship.Logic
{
    public class BattleshipLogicDIModule : NinjectModule
    {
        public override void Load()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfiles(typeof(UserProfile)));
            var mapper = configuration.CreateMapper();

            this.Bind<IMapper>().ToConstant(mapper)
                .When(r => r.ParentContext != null && r.ParentContext.Plan.Type.Namespace.StartsWith("AliaksNad.Battleship"));

            this.Bind<UsersContext>().ToSelf();
            this.Bind<FleetContext>().ToSelf();

            this.Bind<IValidator<UserDto>>().To<UserDtoValidator>();

            this.Bind<IUserService>().ToMethod(ctx =>
            {
                var service = new UserService(ctx.Kernel.Get<UsersContext>(), ctx.Kernel.Get<IMapper>());
                return new ProxyGenerator().CreateInterfaceProxyWithTarget<IUserService>(service, new ValidationInterceptor(ctx.Kernel));
            });
            this.Bind<IGameService>().To<GameService>();
        }
    }
}

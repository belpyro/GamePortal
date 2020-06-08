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
            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(UserProfile), typeof(FleetProfile), typeof(CoordinatesProfile)));
            var mapper = Mapper.Configuration.CreateMapper();

            this.Bind<IMapper>().ToConstant(mapper);
            
            this.Bind<UsersContexts>().ToSelf();
            this.Bind<FleetContexts>().ToSelf();

            this.Bind<IValidator<UserDto>>().To<UserDtoValidator>();

            this.Bind<IUserService>().ToMethod(ctx => 
            {
                var service = new UserService(ctx.Kernel.Get<UsersContexts>(), ctx.Kernel.Get<IMapper>(), ctx.Kernel.Get<ILogger>());
                return new ProxyGenerator().CreateInterfaceProxyWithTarget<IUserService>(service, new ValidationInterceptor(ctx.Kernel));
            });
            this.Bind<IGameService>().To<GameService>();
        }
    }
}

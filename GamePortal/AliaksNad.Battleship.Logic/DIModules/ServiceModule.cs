using AliaksNad.Battleship.Logic.Services;
using Ninject.Modules;
using AliaksNad.Battleship.Logic.Aspects;
using Ninject.Extensions.Interception.Infrastructure.Language;
using AliaksNad.Battleship.Logic.Services.Contracts;

namespace AliaksNad.Battleship.Logic.DIModules
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            var userServiceBinding = this.Bind<IUserService>().To<UserService>();
            userServiceBinding.Intercept().With<ValidationInterceptor>();
            userServiceBinding.Intercept().With<BattleshipLoggerInterceptor>();

            var gameServiceBinding = this.Bind<IGameService>().To<GameService>();
            gameServiceBinding.Intercept().With<BattleshipLoggerInterceptor>();
            gameServiceBinding.Intercept().With<ValidationInterceptor2>();
        }
    }
}

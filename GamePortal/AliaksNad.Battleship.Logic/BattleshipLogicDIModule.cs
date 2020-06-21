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
using System.IO;
using System.Reflection;

namespace AliaksNad.Battleship.Logic
{
    public class BattleshipLogicDIModule : NinjectModule
    {
        public override void Load()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfiles(typeof(UserProfile)));
            var mapper = configuration.CreateMapper();

            this.Bind<IMapper>().ToConstant(mapper)
                .When(r =>
                {
                    return r.ParentContext != null && r.ParentContext.Plan.Type.Namespace.StartsWith("AliaksNad.Battleship");
                });

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var logger = new LoggerConfiguration()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Async(x => x.File(Path.Combine(path, "Logs/Log.txt"), rollOnFileSizeLimit: true, fileSizeLimitBytes: 10_000_000, rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information))
                .WriteTo.File(Path.Combine(path, "Logs/DebugLog.txt"), rollOnFileSizeLimit: true, fileSizeLimitBytes: 10_000_000, rollingInterval: RollingInterval.Day)
                .Enrich.WithHttpRequestType()
                .Enrich.WithWebApiControllerName()
                .Enrich.WithWebApiActionName()
                .MinimumLevel.Debug()
                .CreateLogger();


            this.Bind<ILogger>().ToConstant(logger)
                .When(r =>
                {
                    return r.ParentContext != null && r.ParentContext.Plan.Type.Namespace.StartsWith("AliaksNad.Battleship");
                });

            this.Bind<UsersContext>().ToSelf();
            this.Bind<BattleAreaContext>().ToSelf();

            this.Bind<IValidator<UserDto>>().To<UserDtoValidator>();
            this.Bind<IValidator<BattleAreaDto>>().To<BattleAreaDtoValidator>();

            var userServiceBinding = this.Bind<IUserService>().To<UserService>();
            userServiceBinding.Intercept().With<ValidationInterceptor>();
            userServiceBinding.Intercept().With<BattleshipLoggerInterceptor>();

            var gameServiceBinding = this.Bind<IGameService>().To<GameService>();
            gameServiceBinding.Intercept().With<BattleshipLoggerInterceptor>();
        }
    }
}

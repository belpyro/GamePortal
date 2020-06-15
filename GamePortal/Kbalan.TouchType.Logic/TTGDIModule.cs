using AutoMapper;
using FluentValidation;
using Kbalan.TouchType.Data.Contexts;
using Kbalan.TouchType.Logic.Dto;
using Kbalan.TouchType.Logic.Profiles;
using Kbalan.TouchType.Logic.Services;
using Kbalan.TouchType.Logic.Validators;
using Ninject.Modules;
using Castle.DynamicProxy;
using Ninject;
using Kbalan.TouchType.Logic.Aspects;
using Serilog;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using Serilog.Sinks.MSSqlServer;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Planning;

namespace Kbalan.TouchType.Logic
{
    public class TTGDIModule : NinjectModule
    {
        public override void Load()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfiles(typeof(UserProfile).Assembly));
            
            var mapper = configuration.CreateMapper();
            this.Bind<IMapper>().ToConstant(mapper)
                .When(r =>
                {
                    return r.ParentContext != null && r.ParentContext.Plan.Type.Namespace.StartsWith("Kbalan.TouchType");
                });



            var TTGlogDB = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TouchTypeGameContext;Integrated Security=True;";
            var sinkOpts = new SinkOptions();
            sinkOpts.TableName = "Log";
            sinkOpts.AutoCreateSqlTable = true;
            var columnOpts = new ColumnOptions();
            columnOpts.TimeStamp.NonClusteredIndex = true;
            var TTGlogger = new LoggerConfiguration()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.MSSqlServer(
                        connectionString: TTGlogDB,
                        sinkOptions: sinkOpts,
                        columnOptions: columnOpts)
                .Enrich.WithHttpRequestType()
                .Enrich.WithWebApiControllerName()
                .Enrich.WithWebApiActionName()
                .MinimumLevel.Verbose()
                .CreateLogger();
            this.Bind<ILogger>().ToConstant(TTGlogger);
            this.Bind<TouchTypeGameContext>().ToSelf();
            this.Bind<IValidator<UserSettingDto>>().To<UserSettingDtoValidator>();
            this.Bind<IValidator<UserDto>>().To<UserDtoValidator>();
            this.Bind<IValidator<SettingDto>>().To<SettingDtoValidator>();
            this.Bind<IValidator<StatisticDto>>().To<StatisticDtoValidator>();
            this.Bind<IValidator<TextSetDto>>().To<TextSetDtoValidator>();
            

            var textSetBinding = Bind<ITextSetService>().To<TextSetService>();
            textSetBinding.Intercept().With<TextSetValidationInterceptor>();
            textSetBinding.Intercept().With<LoggerInterceptor>();

            var userBinding = Bind<IUserService>().To<UserService>();
            userBinding.Intercept().With<UserValidationInterceptor>();
            userBinding.Intercept().With<LoggerInterceptor>();

            var settingBinding = Bind<ISettingService>().To<SettingService>();
            userBinding.Intercept().With<SettingValidationInterceptor>();
            userBinding.Intercept().With<LoggerInterceptor>();

            var statisticBinding = Bind<IStatisticService>().To<StatisticService>();
            userBinding.Intercept().With<StatisticValidationInterceptor>();
            userBinding.Intercept().With<LoggerInterceptor>();

        }
    }
}

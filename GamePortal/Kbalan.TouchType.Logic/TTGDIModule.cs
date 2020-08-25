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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Kbalan.Logic;
using Kbalan.TouchType.Data.Models;

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
            this.Bind<IValidator<NewUserDto>>().To<NewUserValidator>();
            this.Bind<IValidator<SettingDto>>().To<SettingDtoValidator>();
            this.Bind<IValidator<StatisticDto>>().To<StatisticDtoValidator>();
            this.Bind<IValidator<TextSetDto>>().To<TextSetDtoValidator>();
            this.Bind<IUploadService>().To<UploadService>();
            this.Bind<ISingleGameService>().To<SingleGameService>();

            var textSetBinding = Bind<ITextSetService>().To<TextSetService>();
            textSetBinding.Intercept().With<TextSetValidationInterceptor>();
            textSetBinding.Intercept().With<LoggerInterceptor>();

            this.Bind<IUserStore<ApplicationUser>>().ToMethod(ctx => new UserStore<ApplicationUser>(ctx.Kernel.Get<TouchTypeGameContext>())).When(r =>
            {
                return r.ParentContext != null && r.ParentContext.Plan.Type.Namespace.StartsWith("Kbalan.TouchType");
            }); ;
            this.Bind<UserManager<ApplicationUser>>().ToMethod(ctx =>
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TouchTypeGameContext()));
                
                manager.EmailService = new EmailService();
                manager.UserValidator = new UserValidator<ApplicationUser>(manager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };
                manager.PasswordValidator = new PasswordValidator()
                {
                    RequireDigit = false,
                    RequiredLength = 3,
                    RequireLowercase = false,
                    RequireNonLetterOrDigit = false,
                    RequireUppercase = false
                };

                manager.UserTokenProvider = new EmailTokenProvider<ApplicationUser>();

                return manager;
            }).When(r =>
            {
                return r.ParentContext != null && r.ParentContext.Plan.Type.Namespace.StartsWith("Kbalan.TouchType");
            });
            var userBinding = Bind<IUserService>().To<UserService>() ;
            userBinding.Intercept().With<UserValidationInterceptor>();
            userBinding.Intercept().With<LoggerInterceptor>();

            var settingBinding = Bind<ISettingService>().To<SettingService>();
            settingBinding.Intercept().With<SettingValidationInterceptor>();
            settingBinding.Intercept().With<LoggerInterceptor>();

            var statisticBinding = Bind<IStatisticService>().To<StatisticService>();
            statisticBinding.Intercept().With<StatisticValidationInterceptor>();
            statisticBinding.Intercept().With<LoggerInterceptor>();

        }
    }
}

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

namespace Kbalan.TouchType.Logic
{
    public class TTGDIModule : NinjectModule
    {
        public override void Load()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfiles(typeof(UserProfile).Assembly));
            
            var mapper = configuration.CreateMapper();
            this.Bind<IMapper>().ToConstant(mapper)
                /*.When(r =>  r.ParentContext.Plan.Type.Namespace.StartsWith("Kbalan.TouchType"))*/;
  


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
            
            this.Bind<ITextSetService>().ToMethod(ctx =>
            {
                var service = new TextSetService(ctx.Kernel.Get<TouchTypeGameContext>(), ctx.Kernel.Get<IMapper>());
                return new ProxyGenerator().CreateInterfaceProxyWithTarget<ITextSetService>(service, new LoggerInterceptor(ctx.Kernel)
                    , new TextSetValidationInterceptor(ctx.Kernel));
            });
            this.Bind<IUserService>().ToMethod(ctx =>
            {
                var service = new UserService(ctx.Kernel.Get<TouchTypeGameContext>(), ctx.Kernel.Get<IMapper>());
                return new ProxyGenerator().CreateInterfaceProxyWithTarget<IUserService>(service, new LoggerInterceptor(ctx.Kernel)
                    , new UserValidationInterceptor(ctx.Kernel));
            });
            this.Bind<ISettingService>().ToMethod(ctx =>
            {
                var service = new SettingService(ctx.Kernel.Get<TouchTypeGameContext>(), ctx.Kernel.Get<IMapper>());
                return new ProxyGenerator().CreateInterfaceProxyWithTarget<ISettingService>(service, new LoggerInterceptor(ctx.Kernel)
                    , new SettingValidationInterceptor(ctx.Kernel));
            });
            this.Bind<IStatisticService>().ToMethod(ctx =>
            {
                var service = new StatisticService(ctx.Kernel.Get<TouchTypeGameContext>(), ctx.Kernel.Get<IMapper>());
                return new ProxyGenerator().CreateInterfaceProxyWithTarget<IStatisticService>(service, new LoggerInterceptor(ctx.Kernel)
                    , new StatisticValidationInterceptor(ctx.Kernel));
            });
        }
    }
}

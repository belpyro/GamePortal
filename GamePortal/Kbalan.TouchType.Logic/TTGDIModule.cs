using AutoMapper;
using FluentValidation;
using Kbalan.TouchType.Data.Contexts;
using Kbalan.TouchType.Data.Models;
using Kbalan.TouchType.Logic.Dto;
using Kbalan.TouchType.Logic.Profiles;
using Kbalan.TouchType.Logic.Services;
using Kbalan.TouchType.Logic.Validators;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Ninject;
using Kbalan.TouchType.Logic.Aspects;
using Serilog;
using System.Reflection;

namespace Kbalan.TouchType.Logic
{
    public class TTGDIModule : NinjectModule
    {
        public override void Load()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfiles(Assembly.GetExecutingAssembly()));
            
            var mapper = configuration.CreateMapper();
            this.Bind<IMapper>().ToConstant(mapper);

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

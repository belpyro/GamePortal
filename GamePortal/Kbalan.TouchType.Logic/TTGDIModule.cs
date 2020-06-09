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

namespace Kbalan.TouchType.Logic
{
    public class TTGDIModule : NinjectModule
    {
        public override void Load()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(typeof(UserProfile)));
            var mapper = configuration.CreateMapper();
            this.Bind<IMapper>().ToConstant(mapper)
                .When(r => r.ParentContext != null && r.ParentContext.Plan.Type.Namespace.StartsWith("Kbalan.TouchType"));

            this.Bind<TouchTypeGameContext>().ToSelf();
            this.Bind<IValidator<UserSettingDto>>().To<UserSettingDtoValidator>();
            this.Bind<IValidator<UserDto>>().To<UserDtoValidator>();
            this.Bind<IValidator<SettingDto>>().To<SettingDtoValidator>();
            this.Bind<IValidator<StatisticDto>>().To<StatisticDtoValidator>();
            this.Bind<IValidator<TextSetDto>>().To<TextSetDtoValidator>();
            this.Bind<ITextSetService>().ToMethod(ctx =>
            {
                var service = new TextSetService(ctx.Kernel.Get<TouchTypeGameContext>(), ctx.Kernel.Get<IMapper>()
                    , ctx.Kernel.Get<IValidator<TextSetDto>>());
                return new ProxyGenerator().CreateInterfaceProxyWithTarget<ITextSetService>(service, new TextSetValidationInterceptor(ctx.Kernel));
            });
            this.Bind<IUserService>().ToMethod(ctx =>
            {
                var service = new UserService(ctx.Kernel.Get<TouchTypeGameContext>(), ctx.Kernel.Get<IMapper>()
                    , ctx.Kernel.Get<IValidator<UserSettingDto>>(), ctx.Kernel.Get<IValidator<UserDto>>());
                return new ProxyGenerator().CreateInterfaceProxyWithTarget<IUserService>(service, new UserValidationInterceptor(ctx.Kernel));
            });
            this.Bind<ISettingService>().ToMethod(ctx =>
            {
                var service = new SettingService(ctx.Kernel.Get<TouchTypeGameContext>(), ctx.Kernel.Get<IMapper>()
                    , ctx.Kernel.Get<IValidator<SettingDto>>());
                return new ProxyGenerator().CreateInterfaceProxyWithTarget<ISettingService>(service, new SettingValidationInterceptor(ctx.Kernel));
            });
            this.Bind<IStatisticService>().ToMethod(ctx =>
            {
                var service = new StatisticService(ctx.Kernel.Get<TouchTypeGameContext>(), ctx.Kernel.Get<IMapper>()
                    , ctx.Kernel.Get<IValidator<StatisticDto>>());
                return new ProxyGenerator().CreateInterfaceProxyWithTarget<IStatisticService>(service, new StatisticValidationInterceptor(ctx.Kernel));
            });
        }
    }
}

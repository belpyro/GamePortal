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
            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(UserProfile)));

            var mapper = Mapper.Configuration.CreateMapper();

            this.Bind<IMapper>().ToConstant(mapper);

            this.Bind<TouchTypeGameContext>().ToSelf();
            this.Bind<ISettingService>().To<SettingService>();
            this.Bind<IStatisticService>().To<StatisticService>();
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
        }
    }
}

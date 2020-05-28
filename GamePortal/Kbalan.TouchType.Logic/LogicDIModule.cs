using AutoMapper;
using Kbalan.TouchType.Data.Contexts;
using Kbalan.TouchType.Data.Models;
using Kbalan.TouchType.Logic.Profiles;
using Kbalan.TouchType.Logic.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic
{
    public class LogicDIModule : NinjectModule
    {
        public override void Load()
        {
            
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<RegisterUserProfile>();
                cfg.AddProfile<TextSetProfile>();
            });
            var mapper = Mapper.Configuration.CreateMapper();

            this.Bind<IMapper>().ToConstant(mapper);

            this.Bind<TouchTypeGameContext>().ToSelf();
            this.Bind<IUserService>().To<UserService>();
            this.Bind<ITextSetService>().To<TextSetService>();
        }
    }
}

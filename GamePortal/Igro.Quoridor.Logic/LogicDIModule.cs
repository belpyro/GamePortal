using AutoMapper;
using GamePortal.Logic.Igro.Quoridor.Logic.Services.User;
using Igro.Quoridor.Data.Contexts;
using Igro.Quoridor.Logic.Profiles;
using Igro.Quoridor.Logic.Services.User;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igro.Quoridor.Logic
{
    public class LogicDIModule : NinjectModule
    {
        public override void Load()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<UserProfile>());
            var mapper = Mapper.Configuration.CreateMapper();

            this.Bind<IMapper>().ToConstant(mapper);

            this.Bind<UserContext>().ToSelf();
            this.Bind<IUserService>().To<UserService>();
        }
    }
}

using GamePortal.Logic.Igro.Quoridor.Logic.Services.User;
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
            this.Bind<IUserService>().To<UserService>();
    }
    }
}

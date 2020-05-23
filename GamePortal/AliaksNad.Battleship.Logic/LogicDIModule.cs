using AliaksNad.Battleship.Data.Contexts;
using AliaksNad.Battleship.Logic.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic
{
    public class LogicDIModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<UsersContexts>().ToSelf();
            this.Bind<IUserService>().To<UserService>();
        }
    }
}

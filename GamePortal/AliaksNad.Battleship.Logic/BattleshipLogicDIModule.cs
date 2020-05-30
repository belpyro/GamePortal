using AliaksNad.Battleship.Data.Contexts;
using AliaksNad.Battleship.Logic.Profiles;
using AliaksNad.Battleship.Logic.Services;
using AutoMapper;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliaksNad.Battleship.Logic
{
    public class BattleshipLogicDIModule : NinjectModule
    {
        public override void Load()
        {
            Mapper.Configuration.AssertConfigurationIsValid();
            var mapper = Mapper.Configuration.CreateMapper();

            this.Bind<IMapper>().ToConstant(mapper);


            this.Bind<UsersContexts>().ToSelf();
            this.Bind<IUserService>().To<UserService>();
            this.Bind<IGameService>().To<GameService>();
        }
    }
}

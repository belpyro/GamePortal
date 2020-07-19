using Ninject.Modules;
using Ninject;
using AliaksNad.Battleship.Logic.DIModules;

namespace AliaksNad.Battleship.Logic
{
    public class BattleshipLogicDIModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Load(
                new MapperModule(),
                new LoggerModule(),
                new ContextModule(),
                new ValidatorModule(),
                new UserManagerModule(),
                new ServiceModule(),
                new IdentityServerModule());
        }
    }
}

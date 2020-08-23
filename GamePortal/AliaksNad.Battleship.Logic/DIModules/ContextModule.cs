using AliaksNad.Battleship.Data.Contexts;
using Ninject.Modules;

namespace AliaksNad.Battleship.Logic.DIModules
{
    public class ContextModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<BattleAreaContext>().ToSelf();
        }
    }
}

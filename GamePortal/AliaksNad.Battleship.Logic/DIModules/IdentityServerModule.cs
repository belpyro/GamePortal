using Ninject.Modules;
using AliaksNad.Battleship.Logic.Configuration;

namespace AliaksNad.Battleship.Logic.DIModules
{
    public class IdentityServerModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<BattleshipIdentityServerConfiguration>().ToSelf();
            this.Bind<BattleshipIdentityServerTokenAuthenticationConfiguration>().ToSelf();
        }
    }
}

using AliaksNad.Battleship.Logic.Profiles;
using Ninject.Modules;
using Serilog;

namespace AliaksNad.Battleship.Logic.DIModules
{
    public class LoggerModule : NinjectModule
    {
        public override void Load()
        {
            var logger = new LoggerProfile().CreateLogger();
            this.Bind<ILogger>().ToConstant(logger)
                .When(r =>
                {
                    return r.ParentContext != null && r.ParentContext.Plan.Type.Namespace.StartsWith("AliaksNad.Battleship");
                });
        }
    }
}

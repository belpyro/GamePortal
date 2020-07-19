using AliaksNad.Battleship.Logic.Profiles;
using AutoMapper;
using Ninject.Modules;

namespace AliaksNad.Battleship.Logic.DIModules
{
    public class MapperModule : NinjectModule
    {
        public override void Load()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfiles(typeof(UserProfile)));
            var mapper = configuration.CreateMapper();

            Bind<IMapper>().ToConstant(mapper)
                .When(r =>
                {
                    return r.ParentContext != null && r.ParentContext.Plan.Type.Namespace.StartsWith("AliaksNad.Battleship");
                });
        }
    }
}

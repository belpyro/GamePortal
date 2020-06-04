using AutoMapper;
using Ninject.Modules;
using Vitaly.Sapper.Data.Contexts;
using Vitaly.Sapper.Logic.Profiles;
using Vitaly.Sapper.Logic.Services;

namespace Vitaly.Sapper.Logic
{
    public class VitalySapperLogicDIModule : NinjectModule
    {
        public override void Load()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.AddProfile<VitalySapperUserProfile>());
            var mapper = Mapper.Configuration.CreateMapper();

            this.Bind<IMapper>().ToConstant(mapper);

            this.Bind<SapperContext>().ToSelf();
            this.Bind<ISapperService>().To<SapperService>();
        }
    }
}

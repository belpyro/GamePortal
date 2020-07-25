using FluentValidation;
using Ninject.Modules;
using System.Reflection;

namespace AliaksNad.Battleship.Logic.DIModules
{
    public class ValidatorModule : NinjectModule
    {
        public override void Load()
        {
            AssemblyScanner
                .FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
                .ForEach(result => Bind(result.InterfaceType)
                .To(result.ValidatorType)
                .InSingletonScope());
        }
    }
}

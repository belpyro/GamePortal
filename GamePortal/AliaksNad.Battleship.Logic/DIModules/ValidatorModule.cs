using FluentValidation;
using Ninject.Modules;
using AliaksNad.Battleship.Logic.Models.User;
using AliaksNad.Battleship.Logic.Models.Game;
using AliaksNad.Battleship.Logic.Validators.Game;
using AliaksNad.Battleship.Logic.Validators.User;

namespace AliaksNad.Battleship.Logic.DIModules
{
    public class ValidatorModule : NinjectModule
    {
        public override void Load()
        {
            //AssemblyScanner
            //    .FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
            //    .ForEach(result => Bind(result.InterfaceType)
            //    .To(result.ValidatorType)
            //    .InSingletonScope());

            this.Bind<IValidator<UserDto>>().To<UserDtoValidator>();
            this.Bind<IValidator<CoordinatesDto>>().To<CoordinatesDtoValidator>();
            this.Bind<IValidator<TargetDto>>().To<TargetDtoValidator>();
        }
    }
}

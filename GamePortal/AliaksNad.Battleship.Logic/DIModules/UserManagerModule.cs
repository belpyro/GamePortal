using AliaksNad.Battleship.Logic.Services;
using Ninject.Modules;
using Ninject;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AliaksNad.Battleship.Data.Contexts;

namespace AliaksNad.Battleship.Logic.DIModules
{
    public class UserManagerModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IUserStore<IdentityUser>>().ToMethod(ctx => new UserStore<IdentityUser>(ctx.Kernel.Get<BattleAreaContext>()));

            this.Bind<UserManager<IdentityUser>>().ToMethod(ctx =>
            {
                var manager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(ctx.Kernel.Get<BattleAreaContext>()));
                manager.UserValidator = new UserValidator<IdentityUser>(manager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };
                manager.PasswordValidator = new PasswordValidator()
                {
                    RequireDigit = false,
                    RequiredLength = 3,
                    RequireLowercase = false,
                    RequireNonLetterOrDigit = false,
                    RequireUppercase = false
                };
                manager.EmailService = new BattleshipEmailService();

                manager.UserTokenProvider = new EmailTokenProvider<IdentityUser>();

                return manager;
            });
        }
    }
}

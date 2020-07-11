using AliaksNad.Battleship.Logic.Configuration;
using GamePortal.Web.Api.Config;
using Ninject;
using Owin;

namespace GamePortal.Web.Api.Middleware
{
    internal static class BattleshipIdentityServerExtension
    {
        public static IAppBuilder UseBattleshipIdentityServer (this IAppBuilder app, BattleshipIdentityServerConfiguration configuration)
        {
            //var configuration = kernel.Get<BattleshipIdentityServerConfiguration>();
            var result = configuration.Getoptions();

            result.SigningCertificate = Certificate.Get();

            app.UseIdentityServer(result);

            return app;
        }
    }
}
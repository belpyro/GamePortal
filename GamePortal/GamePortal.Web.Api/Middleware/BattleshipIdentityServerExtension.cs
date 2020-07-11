using AliaksNad.Battleship.Logic.Configuration;
using GamePortal.Web.Api.Config;
using IdentityServer3.AccessTokenValidation;
using Ninject;
using Owin;

namespace GamePortal.Web.Api.Middleware
{
    internal static class BattleshipIdentityServerExtension
    {
        public static IAppBuilder UseBattleshipIdentityServer (this IAppBuilder app, BattleshipIdentityServerConfiguration IdentServConfig,
            BattleshipIdentityServerTokenAuthenticationConfiguration identTokenConf)
        {
            var option = IdentServConfig.Getoptions();
            option.SigningCertificate = Certificate.Get();

            app.UseIdentityServer(option);

            var config = identTokenConf.Get();
            config.SigningCertificate = Certificate.Get();
            app.UseIdentityServerBearerTokenAuthentication(config);

            return app;
        }
    }
}
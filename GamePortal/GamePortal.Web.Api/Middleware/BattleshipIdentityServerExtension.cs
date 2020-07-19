using AliaksNad.Battleship.Logic.Configuration;
using GamePortal.Web.Api.Config;
using Ninject;
using Owin;

namespace GamePortal.Web.Api.Middleware
{
    internal static class BattleshipIdentityServerExtension
    {
        public static IAppBuilder UseBattleshipIdentityServer(this IAppBuilder app, IKernel kernel)
        {
            var identityServerConfig = kernel.Get<BattleshipIdentityServerConfiguration>();
            var option = identityServerConfig.Getoptions();
            option.SigningCertificate = Certificate.Get();
            app.UseIdentityServer(option);

            var serverTokenConfig = kernel.Get<BattleshipIdentityServerTokenAuthenticationConfiguration>();
            var config = serverTokenConfig.Get();
            config.SigningCertificate = Certificate.Get();
            app.UseIdentityServerBearerTokenAuthentication(config);

            return app;
        }
    }
}
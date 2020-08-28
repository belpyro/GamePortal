using AliaksNad.Battleship.Logic.Configuration;
using GamePortal.Web.Api.Config;
using Microsoft.Owin.Security.Google;
using Ninject;
using Owin;

namespace GamePortal.Web.Api.Middleware
{
    internal static class BattleshipIdentityServerExtension
    {
        public static IAppBuilder UseBattleshipIdentityServer(this IAppBuilder app, IKernel kernel)
        {
            UseBattleshipIS(app, kernel);
            UseBattleshipISAuthentication(app, kernel);
            UseBattleshipExternalLogin(app, kernel);

            return app;
        }

        private static IAppBuilder UseBattleshipIS(this IAppBuilder app, IKernel kernel)
        {
            var identityServerConfig = kernel.Get<BattleshipISConfig>();
            var option = identityServerConfig.Getoptions();
            option.SigningCertificate = Certificate.Get();
            app.UseIdentityServer(option);

            return app;
        }

        private static IAppBuilder UseBattleshipISAuthentication(this IAppBuilder app, IKernel kernel)
        {
            var serverTokenConfig = kernel.Get<BattleshipISAuthConfig>();
            var config = serverTokenConfig.Get();
            config.SigningCertificate = Certificate.Get();
            app.UseIdentityServerBearerTokenAuthentication(config);

            return app;
        }

        private static IAppBuilder UseBattleshipExternalLogin(this IAppBuilder app, IKernel kernel)
        {
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "241000708571-qhb5s5fqe1nin8s33isgvmpfosa0cgpt.apps.googleusercontent.com",
                ClientSecret = "G3VZRIXRrewqBkFlRKQtLN3o",
                AuthenticationType = "Google"
            });

            app.Map("/login/google", x => x.Use<GoogleAuthMiddleware>());

            return app;
        }
    }
}
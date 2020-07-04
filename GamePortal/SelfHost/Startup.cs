using IdentityServer3.Core;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Google;
using Owin;
using System.Collections.Generic;
using AliaksNad.Battleship.IdentityServer3.SelfHost.Config;

namespace AliaksNad.Battleship.IdentityServer3.SelfHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var factory = new IdentityServerServiceFactory();

            var client = new Client
            {
                ClientId = "BattleshipWebClient",
                ClientSecrets = new List<Secret> { new Secret("secret".Sha256()) },
                AllowAccessToAllScopes = true,
                ClientName = "Battleship Web Client",
                Flow = Flows.AuthorizationCode,
                RedirectUris = new List<string>() { "https://localhost:44333" }
            };

            var user = new InMemoryUser()
            {
                Username = "user",
                Password = "666",
                Subject = "132-456-789"
            };

            factory
                .UseInMemoryScopes(StandardScopes.All)
                .UseInMemoryClients(new[] { client })
                .UseInMemoryUsers(new List<InMemoryUser>() { user });

            var options = new IdentityServerOptions
            {
                EnableWelcomePage = true,
#if DEBUG
                RequireSsl = false,
#endif
                LoggingOptions = new LoggingOptions
                {
                    EnableHttpLogging = true,
                    EnableKatanaLogging = true,
                    EnableWebApiDiagnostics = true,
                    WebApiDiagnosticsIsVerbose = true
                },
                SiteName = "Battleship",

                SigningCertificate = Certificate.Get(),

                AuthenticationOptions = new AuthenticationOptions
                {
                    EnablePostSignOutAutoRedirect = true,
                    IdentityProviders = ConfigureIdentityProviders
                },

                Factory = factory,
            };
            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseIdentityServer(options);
        }

        private void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                AuthenticationType = "Google",
                Caption = "Sign-in with Google",
                SignInAsAuthenticationType = signInAsType,

                ClientId = "241000708571-qhb5s5fqe1nin8s33isgvmpfosa0cgpt.apps.googleusercontent.com",
                ClientSecret = "G3VZRIXRrewqBkFlRKQtLN3o",
            });
        }
    }
}
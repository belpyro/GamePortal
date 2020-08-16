using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using System.Collections.Generic;
using IdentityServer3.Core.Services;
using IdentityServer3.AspNetIdentity;
using Owin;
using Microsoft.Owin.Security.Google;


namespace AliaksNad.Battleship.Logic.Configuration
{
    public sealed class BattleshipIdentityServerConfiguration
    {
        private readonly UserManager<IdentityUser> _userManager;

        public BattleshipIdentityServerConfiguration(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
        }

        public IdentityServerOptions Getoptions()
        {
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

                AuthenticationOptions = new AuthenticationOptions
                {
                    EnablePostSignOutAutoRedirect = true,
                    IdentityProviders = ConfigureGoogleAuthentication
                },

                Factory = GetFactory()
            };

            return options;
        }

        private IdentityServerServiceFactory GetFactory()
        {
            var factory = new IdentityServerServiceFactory();

            var client = new Client
            {
                ClientId = "BattleshipWebClient",
                ClientSecrets = new List<Secret> { new Secret("secret".Sha256()) },
                AllowAccessToAllScopes = true,
                ClientName = "Battleship Web Client",
                Flow = Flows.AuthorizationCode,
                RedirectUris = new List<string>() { "https://localhost:44555", "http://localhost:4200/index.html" }
            };

            var userClient = new Client
            {
                ClientId = "BattleshipUserClient",
                ClientSecrets = new List<Secret> { new Secret("secret".Sha256()) },
                AllowAccessToAllScopes = true,
                ClientName = "Battleship Web Client",
                Flow = Flows.ResourceOwner,
                RedirectUris = new List<string>() { "https://localhost:44555", "http://localhost:4200/index.html" }
            };

            factory
                .UseInMemoryScopes(StandardScopes.All)
                .UseInMemoryClients(new[] { client, userClient })
                .UserService = new Registration<IUserService>(new AspNetIdentityUserService<IdentityUser, string>(_userManager));

            return factory;
        }

        private void ConfigureGoogleAuthentication(IAppBuilder app, string signInAsType)
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

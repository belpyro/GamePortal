using IdentityServer3.Core;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using Owin;
using SelfHost.Config;
using System.Collections.Generic;

namespace SelfHost
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

            factory.UseInMemoryScopes(StandardScopes.All)
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
                Factory = factory,
            };

            app.UseIdentityServer(options);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Cors;
using AutoMapper.Execution;
using IdentityServer3.AspNetIdentity;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.InMemory;
using IdentityServer3.EntityFramework;
using Kbalan.TouchType.Logic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Ninject;
using Owin;

[assembly: OwinStartup(typeof(KBalan.IdentityServerHost.Startup))]

namespace KBalan.IdentityServerHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var provide = new CorsPolicyProvider
            {
                PolicyResolver = ctx => Task.FromResult(new CorsPolicy
                {
                    AllowAnyHeader = true,
                    AllowAnyMethod = true,
                    AllowAnyOrigin = true
                })
            };
            app.UseCors(new CorsOptions { PolicyProvider = provide });
            IdentityServerServiceFactory factory = new IdentityServerServiceFactory();
            var client = new Client()
            {
                ClientId = "TTGWebClient",
                ClientSecrets = new List<Secret>() { new Secret("secret".Sha256()) },
                AllowAccessToAllScopes = true,
                ClientName = "TTG Web Client",
                Flow = Flows.AuthorizationCode,
                RedirectUris = new List<string>() { "https://localhost:5555", "http://localhost:4200/index.html" },
                PostLogoutRedirectUris = new List<string>() { "https://localhost:5555", "http://localhost:4200/entry/login" }
            };

            var userClient = new Client()
            {
                ClientId = "TTGUserClient",
                ClientSecrets = new List<Secret>() { new Secret("secret".Sha256()) },
                AllowAccessToAllScopes = true,
                ClientName = "TTG Web Client",
                Flow = Flows.ResourceOwner,
                RedirectUris = new List<string>() { "https://localhost:5555", "http://localhost:4200/index.html" },
                PostLogoutRedirectUris = new List<string>() { "https://localhost:5555", "http://localhost:4200/entry/login" }
            };


            factory.UseInMemoryScopes(StandardScopes.All.Append(
                    new Scope() { Name = "api", DisplayName = "Api", Description = "Access to API", Type = ScopeType.Resource, Claims = new List<ScopeClaim> { new ScopeClaim("api-version", true) } }))

            .UseInMemoryClients(new[] { client, userClient });

            factory.UserService = new Registration<IUserService>(UserServiceFactory.Create());

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ApplicationCookie);


            app.UseIdentityServer(new IdentityServerOptions
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
                    WebApiDiagnosticsIsVerbose = true,

                },
                AuthenticationOptions = new AuthenticationOptions
                {
                    CookieOptions = new IdentityServer3.Core.Configuration.CookieOptions
                    {
                        SlidingExpiration = true,
                        IsPersistent = true,
                        
                    },
                    RequireSignOutPrompt = true,
                },
                SiteName = "TouchTypeGame",
                Factory = factory,
                SigningCertificate = LoadCertificate()
            }); 

        }

        private static X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Config\idsrv3test.pfx"), "idsrv3test");
        }
    }
}

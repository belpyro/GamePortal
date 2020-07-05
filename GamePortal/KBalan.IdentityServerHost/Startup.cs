using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
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
using Ninject;
using Owin;

[assembly: OwinStartup(typeof(KBalan.IdentityServerHost.Startup))]

namespace KBalan.IdentityServerHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseWelcomePage();
            IdentityServerServiceFactory factory = new IdentityServerServiceFactory();
            var client = new Client()
            {
                ClientId = "TTGWebClient",
                ClientSecrets = new List<Secret>() { new Secret("secret".Sha256()) },
                AllowAccessToAllScopes = true,
                ClientName = "TTG Web Client",
                Flow = Flows.AuthorizationCode,
                RedirectUris = new List<string>() { "https://localhost:5555" }
            };
            var user = new InMemoryUser()
            {
                Username = "user",
                Password = "123",
                Subject = "123-123-123",
                Claims = new[] { new Claim("api-version", "1") }
            };



            factory.UseInMemoryScopes(StandardScopes.All.Append(
                new Scope()
                {
                    Name = "api",
                    Type = ScopeType.Identity,
                    Claims = new List<ScopeClaim> { new ScopeClaim("api-version", true) }
                }))
                .UseInMemoryUsers(new List<InMemoryUser>() { user })
            .UseInMemoryClients(new[] { client });

            //factory.UserService = new Registration<IUserService>(UserServiceFactory.Create());


            //factory.UserService = new Registration<IUserService>(new AspNetIdentityUserService<IdentityUser, string>(kernel.Get<UserManager<IdentityUser>>()));

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
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

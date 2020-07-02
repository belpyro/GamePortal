using System;
using System.Threading.Tasks;
using Igro.Quoridor.Logic;
using Igro.Quoridor.Logic.Services;
using AliaksNad.Battleship.Logic;
using Kbalan.TouchType.Logic;
using System.Web.Http;
using FluentValidation.WebApi;
using Microsoft.Owin;
using Ninject;
using Owin;
using NSwag.AspNet.Owin;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Ninject.Web.Common;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Google;
using GamePortal.Web.Api.Middleware;
using Owin.Security.Providers.VKontakte;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using System.Security.Claims;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Linq;
using IdentityServer3.AccessTokenValidation;
using IdentityServer3.AspNetIdentity;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartup(typeof(GamePortal.Web.Api.Startup))]

namespace GamePortal.Web.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var kernel = new StandardKernel(new NinjectSettings { LoadExtensions = true });

            kernel.Load(new LogicDIModule(), new TTGDIModule(), new BattleshipLogicDIModule());

            FluentValidationModelValidatorProvider.Configure(config, opt =>
            {
                opt.ValidatorFactory = new CustomValidatorFactory(kernel);
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "719719063561-v05dg5416mu8km1u2filstn03oqj98s4.apps.googleusercontent.com",
                ClientSecret = "n8sW2lGlSM7QsayPw97knojT",
                AuthenticationType = "MyGoogle"
            });

            app.UseVKontakteAuthentication(new VKontakteAuthenticationOptions
            {
                ClientId = "7526371",
                ClientSecret = "Z3blscBduDFc17p8NpWw",
                AuthenticationType = "MyVk",
                Scope = { "email" }
            });

            app.Map("/login/google", b => b.Use<GoogleAuthMiddleWare>());
            app.Map("/login/vk", b => b.Use<VkAuthMiddleWare>());

            IdentityServer3.Core.Configuration.IdentityServerServiceFactory factory = new IdentityServerServiceFactory();
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
                .UseInMemoryClients(new[] { client });
                 factory.UserService = new Registration<IdentityServer3.Core.Services.IUserService>(new AspNetIdentityUserService<IdentityUser, string>(kernel.Get<UserManager<IdentityUser>>()));

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
                    WebApiDiagnosticsIsVerbose = true
                },
                SiteName = "TouchTypeGame",
                Factory = factory,
                SigningCertificate = LoadCertificate()
            }).UseIdentityServerBearerTokenAuthentication(new IdentityServer3.AccessTokenValidation.IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "https://localhost:44313/",
                ClientId = "TTGWebClient",
                ClientSecret = "secret",
                RequireHttps = false,
                ValidationMode = ValidationMode.Local,
                IssuerName = "https://localhost:44313/",
                SigningCertificate = LoadCertificate(),
                ValidAudiences = new[] { "https://localhost:44313/resourses" }
            }) ;

            app.UseSwagger(typeof(Startup).Assembly).UseSwaggerUi3()
                .UseNinjectMiddleware(() => kernel).UseNinjectWebApi(config);            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }

        private static X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\Config\idsrv3test.pfx"), "idsrv3test");
        }
    }
}

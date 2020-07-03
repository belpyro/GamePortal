using System;
using System.IO;
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
using System.Web.Http.ExceptionHandling;
using Elmah.Contrib.WebApi;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Google;
using GamePortal.Web.Api.Middleware;
using IdentityServer3.AccessTokenValidation;
using System.Security.Cryptography.X509Certificates;
using GamePortal.Web.Api.Config;

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

            config.Services.Replace(typeof(IExceptionLogger), new ElmahExceptionLogger());   // Replace system logger for elmarh

            FluentValidationModelValidatorProvider.Configure(config, opt =>
            {
                opt.ValidatorFactory = new CustomValidatorFactory(kernel);
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "241000708571-qhb5s5fqe1nin8s33isgvmpfosa0cgpt.apps.googleusercontent.com",
                ClientSecret = "G3VZRIXRrewqBkFlRKQtLN3o"
            });

            app.Map("/login/google", x => x.Use<GoogleAuthMiddleware>());

            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "https://localhost:44333/core",
                ClientId = "BattleshipWebClient",
                ClientSecret = "secret",
                RequireHttps = false,
                ValidationMode = ValidationMode.Local,
                IssuerName = "https://localhost:44333/core",
                SigningCertificate = Certificate.Get(),
                ValidAudiences = new[] { "https://localhost:44333/core/resources" }
            });

            app.UseSwagger(typeof(Startup).Assembly).UseSwaggerUi3()
                .UseNinjectMiddleware(() => kernel).UseNinjectWebApi(config);            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}

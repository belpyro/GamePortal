using Igro.Quoridor.Logic;
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
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Google;
using GamePortal.Web.Api.Middleware;
using Owin.Security.Providers.VKontakte;
using IdentityServer3.AccessTokenValidation;
using System.Web.Http.ExceptionHandling;
using Elmah.Contrib.WebApi;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Cors;
using System.Web.Cors;
using System.Threading.Tasks;
using GamePortal.Web.Api.Config;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Owin.StaticFiles;
using Microsoft.Extensions.FileProviders;

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

            app.MapSignalR(new HubConfiguration { EnableDetailedErrors = true});

            app.UseStaticFiles();
            app.UseSwagger(typeof(Startup).Assembly).UseSwaggerUi3();

            FluentValidationModelValidatorProvider.Configure(config, opt =>
            {
                opt.ValidatorFactory = new CustomValidatorFactory(kernel);
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        });

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "719719063561-v05dg5416mu8km1u2filstn03oqj98s4.apps.googleusercontent.com",
                ClientSecret = "n8sW2lGlSM7QsayPw97knojT",
                AuthenticationType = "TTGGoogle"
            });

            app.UseVKontakteAuthentication(new VKontakteAuthenticationOptions
            {
                ClientId = "7526371",
                ClientSecret = "Z3blscBduDFc17p8NpWw",
                AuthenticationType = "TTGVk",
                Scope = { "email" }
            });

            app.Map("/ttg/login/google", b => b.Use<TTGGoogleAuthMiddleWare>());
            app.Map("/ttg/login/vk", b => b.Use<TTGVkAuthMiddleWare>());

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


            app.UseBattleshipIdentityServer(kernel);

            //TTG External Authentification Options
            app.UseIdentityServerBearerTokenAuthentication(TTGAuthOptionsFactory.GetIdentityServerBearerTokenAuthenticationOptions());
            app.UseGoogleAuthentication(TTGAuthOptionsFactory.GetGoogleAuthenticationOptions());
            app.UseVKontakteAuthentication(TTGAuthOptionsFactory.GetVKontakteAuthenticationOptions());

            //TTG External Authentification Maps
            app.Map("/ttg/login/google", b => b.Use<TTGGoogleAuthMiddleWare>());
            app.Map("/ttg/login/vk", b => b.Use<TTGVkAuthMiddleWare>());

            app.UseNinjectMiddleware(() => kernel).UseNinjectWebApi(config);
        }
    }
}

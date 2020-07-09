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
using System.Web.Http.ExceptionHandling;
using Elmah.Contrib.WebApi;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Cors;
using System.Web.Cors;

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

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
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

            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
                {
                    Authority = "http://localhost:10000/",
                    ClientId = "TTGWebClient",
                    ClientSecret = "secret",
                    RequireHttps = false,
                    ValidationMode = ValidationMode.Local,
                    IssuerName = "http://localhost:10000/",
                    ValidAudiences = new[] { "http://localhost:10000/resources" }
                }) ;

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });


            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "241000708571-qhb5s5fqe1nin8s33isgvmpfosa0cgpt.apps.googleusercontent.com",
                ClientSecret = "G3VZRIXRrewqBkFlRKQtLN3o"
            });
            var provide = new CorsPolicyProvider();
            provide.PolicyResolver = ctx => Task.FromResult(new CorsPolicy
            {
                AllowAnyHeader = true,
                AllowAnyMethod = true,
                AllowAnyOrigin = true
            }); 
            app.UseCors(new CorsOptions { PolicyProvider = provide});

            app.Map("/login/google", x => x.Use<GoogleAuthMiddleware>());

            app.UseSwagger(typeof(Startup).Assembly).UseSwaggerUi3()
                .UseNinjectMiddleware(() => kernel).UseNinjectWebApi(config);          
        }

        private static X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\Config\idsrv3test.pfx"), "idsrv3test");
        }
    }
}

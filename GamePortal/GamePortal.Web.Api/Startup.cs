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
using System.Web.Http.ExceptionHandling;
using Elmah.Contrib.WebApi;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using GamePortal.Web.Api.Middleware;

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

            app.UseSwagger(typeof(Startup).Assembly).UseSwaggerUi3();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });

            app.UseBattleshipIdentityServer(kernel);

            app.UseNinjectMiddleware(() => kernel).UseNinjectWebApi(config);            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}

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
using Vitaly.Sapper.Logic;

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

            kernel.Load(new LogicDIModule(), new TTGDIModule(), new BattleshipLogicDIModule(), new VitalySapperLogicDIModule());

            FluentValidationModelValidatorProvider.Configure(config, opt =>
            {
                opt.ValidatorFactory = new CustomValidatorFactory(kernel);
            });

            app.UseSwagger(typeof(Startup).Assembly).UseSwaggerUi3()
                .UseNinjectMiddleware(() => kernel).UseNinjectWebApi(config);            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}

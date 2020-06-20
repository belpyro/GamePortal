/*[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(GamePortal.Web.Api.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(GamePortal.Web.Api.App_Start.NinjectWebCommon), "Stop")]
*/
namespace GamePortal.Web.Api.App_Start
{
    using System;
    using System.Web;
    using Igro.Quoridor.Logic;
    using Igro.Quoridor.Logic.Services;
    using AliaksNad.Battleship.Logic;
    using Kbalan.TouchType.Logic;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Serilog;
    using System.Reflection;
    using System.IO;
    using Ninject.Extensions.Interception;
    using System.Web.Http;
    using Ninject.Web.WebApi;
    using System.Web.Http.Dependencies;
    using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
    using Serilog.Sinks.MSSqlServer;


    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel(new NinjectSettings { LoadExtensions = true });
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {

            kernel.Load(new LogicDIModule(), new TTGDIModule(), new BattleshipLogicDIModule());
        }
    }
}

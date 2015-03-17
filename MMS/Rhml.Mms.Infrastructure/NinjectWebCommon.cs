using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Web;


[assembly: WebActivator.PreApplicationStartMethod(typeof(Rhml.Mms.Infrastructure.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Rhml.Mms.Infrastructure.NinjectWebCommon), "Stop")]

namespace Rhml.Mms.Infrastructure
{
    /// <summary> Utility class for application dependency injection control using Ninject.
    /// The API for creating, starting, stopping, and configuring ninject in the application.
    /// </summary>
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary> Configure and start the ninject kernel (application DI) so we can
        /// dependency inject our application components in the running program
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary> Stop dependency injection (normally during application shutdown)
        /// from ninject
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary> Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary> Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //kernel.Bind<Rhml.Mms.Interfaces.IServiceLocator>()
            //    .ToMethod(ctx => new ServiceLocator(kernel)).InSingletonScope();
            //    //.To<ServiceLocator>().InSingletonScope();
            kernel.Load(new ServiceLocator());

        }

        // NOTE - some scoping options for ninject
        // .InRequestScope() - shared value managed for each request (IIS) - dispose at end of request processing
        // .InSingletonScope() - single shared instance (disposed with _kernel)
        // .InTransientScope() - default scope - new instance for each call (never disposed)
        // .InThreadScope() - one instance per thread (dispose on thread dispose)
        // note: there are NamedScope extensions for other object scoping
    }
}

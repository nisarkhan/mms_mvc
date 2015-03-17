using Republic.Core.Data.Interfaces;
using Republic.Core.Interfaces;
using Republic.Core.Web.Mvc;
using Republic.Core.Web.Mvc.Interfaces;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Mvc.FilterBindingSyntax;
using System.Data.Entity;
using System;
using Rhml.Mms.Interfaces;
using Rhml.Mms.Security;

namespace Rhml.Mms.Infrastructure
{
    /// <summary> Implements the IServiceLocator design pattern.  An added dependency injection
    /// point to locate injected dependencies into the application
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")] // class is injected
    internal class ServiceLocator : Ninject.Modules.NinjectModule, IServiceLocator
    {

        /// <summary> Initializes a new instance of the <see cref="ServiceLocator"/> class.
        /// </summary>
        public ServiceLocator()
        { }

        /// <summary> Retrieve the specified service
        /// </summary>
        /// <typeparam name="EService">The service type to retrieve</typeparam>
        /// <returns>The specified service</returns>
        public EService Locate<EService>()
        {
            return Kernel.Get<EService>();
        }

        // note: Ninject.Modules.NinjectModule.Load()

        /// <summary> Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {            
            //Main Service Locator Binding
            Bind<IServiceLocator>().ToMethod(context => this).InSingletonScope();

            LoadMvcFilterAttributes();

            // Lazy binding (http://stackoverflow.com/a/12376234/618281)
            Bind(typeof(Lazy<>)).ToMethod(ctx =>
                    GetType()
                        .GetMethod("GetLazyProvider", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                        .MakeGenericMethod(ctx.GenericArguments[0])
                        .Invoke(this, new object[] { ctx.Kernel }));

            //Infrastrcture Bindings
            Bind<Republic.Core.Interfaces.ILogger>()
                        .To<Rhml.Mms.Infrastructure.Logging.Log4NetAppLogger>()
                        .InRequestScope();


            //Security Bindings - use stub security initially
            if (true)
            {
                Bind<Republic.Core.Security.Interfaces.IAppSecurity>()
                            .To<StubAppSecurity>()
                            .InSingletonScope(); 
            }
            else
            {
                Bind<Republic.Core.Security.Interfaces.IAppSecurity>()
                            .To<ClaimSecurity>()
                            .InSingletonScope(); 
            }

            //Business Bindings
            PerformBusinessBinding();
            //PerformInfrastructureBinding();
            //Data Bindings
            PerformDataBinding();
        }

        private void PerformBusinessBinding()
        {
            //Business Bindings
            Bind<Rhml.Mms.Business.Interfaces.IRealtorService>().To<Rhml.Mms.Business.RealtorService>();
            Bind<Rhml.Mms.Business.Interfaces.IBusinessState>().To<Rhml.Mms.Business.BusinessState>().InRequestScope();
            Bind<Rhml.Mms.Business.Interfaces.IRealtorValidator>().To<Rhml.Mms.Business.Validation.RealtorValidator>().InRequestScope();
        }

        private void PerformDataBinding()
        {
            //Data Bindings
            Bind<Rhml.Mms.Data.MmsModels>().ToSelf().InRequestScope();
            Bind(typeof(IRepository<>)).To(typeof(Rhml.Mms.Data.DbSetRepository<>));
            Bind<IUnitOfWork>().To<Rhml.Mms.Data.WorkContainer>();
        }

        private void PerformInfrastructureBinding()
        {
            //Infrastrcture Bindings
            Bind<IAppConfiguration>().To<AppConfiguration>().InRequestScope();
            Bind<Republic.Core.Interfaces.ILogger>()
                        .To<Rhml.Mms.Infrastructure.Logging.Log4NetAppLogger>()
                        .InRequestScope();
        }

        private void LoadMvcFilterAttributes()
        {
            // application filter bindings 
            Kernel.BindFilter<AppLogFilter>(System.Web.Mvc.FilterScope.Action, 0)
                .WhenActionMethodHas<AppLogAttribute>()
                .WithConstructorArgumentFromActionAttribute<AppLogAttribute>(
                    "logLevel",
                    attribute => attribute.LogLevel
                );

            Kernel.BindFilter<AppLogFilter>(System.Web.Mvc.FilterScope.Action, 0)
                .WhenControllerHas<AppLogAttribute>()
                .WithConstructorArgumentFromActionAttribute<AppLogAttribute>(
                    "logLevel",
                    attribute => attribute.LogLevel
                );

            // security filter bindings ... 
            //      on action, then on controller
            //      if roles, use them
            //      if not, then there are no roles enforced (simple authentication only)
            Kernel.BindFilter<AppAuthorizeFilter>(System.Web.Mvc.FilterScope.Controller, 0)
                .WhenActionMethodHas<AppAuthorizeAttribute>()
                .WithConstructorArgumentFromActionAttribute<AppAuthorizeAttribute>(
                    "roles",
                    attribute => attribute.Roles
                );

            Kernel.BindFilter<AppAuthorizeFilter>(System.Web.Mvc.FilterScope.Controller, 0)
                .WhenControllerHas<AppAuthorizeAttribute>()
                .WithConstructorArgumentFromActionAttribute<AppAuthorizeAttribute>(
                    "roles",
                    attribute => attribute.Roles
                );
        }

        private Lazy<T> GetLazyProvider<T>(IKernel k)
        {
            return new Lazy<T>(() => k.Get<T>());
        }

        
 
    }
}

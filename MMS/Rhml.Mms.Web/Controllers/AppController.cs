using Republic.Core.Interfaces;
using Republic.Core.Security.Interfaces;
using System;
using System.Web.Mvc;
using Rhml.Mms.Business.Interfaces;

namespace Rhml.Mms.Web.Controllers
{
    /// <summary>
    /// Base controller every controller in the application should derive from.
    /// </summary>
    public abstract class AppController : Controller
    {

        protected IServiceLocator Service { get; private set; }
        protected IAppUser CurrentUser { get; private set; }

        protected IBusinessState State { get; private set; }
        protected AppController() { throw new NotSupportedException("Controller must be created with the Service Locator."); }

        protected AppController(IServiceLocator service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
            Service = service;
            CurrentUser = service.Locate<IAppSecurity>().GetCurrentUser();
            this.State = service.Locate<IBusinessState>();
            ViewBag.CurrentUser = CurrentUser;
        }

        protected bool PopulateAndCheckBusinessState()
        {
            if (!State.IsValid)
            {
                foreach (var v in State.ValidationResults)
                {
                    ModelState.AddModelError(string.Empty, v.Message);
                }
            }
            return State.IsValid;
        }

    }
}

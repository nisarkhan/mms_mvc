using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Republic.Core.Interfaces;
using Republic.Core.Security.Interfaces;
using Rhml.Mms.Business.Interfaces;


namespace Rhml.Mms.Business
{
    public abstract class BusinessBase
    {
        protected IAppSecurity Security { get; private set; }
        protected IAppUser CurrentUser
        {
            get
            {
                //if (State.IsImpersonatingSystemUser) return Security.SystemUser;
                return Security.GetCurrentUser();
                //return Security.SystemUser;
            }
        }
        protected IBusinessState BusinessState { get; private set; }
        protected IServiceLocator Service { get; private set; }
        protected BusinessBase(IServiceLocator service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
            Service = service;
            Security = service.Locate<IAppSecurity>();
            BusinessState = service.Locate<IBusinessState>();
        }

        // TODO: Enable auditing for changes
    }
}

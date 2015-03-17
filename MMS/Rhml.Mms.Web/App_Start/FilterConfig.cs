using System;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Authorization.Mvc;

namespace Rhml.Mms.Web
{
    // from the web template ... retain the structure and suppress the warning
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors")]
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException("filters");
            }
            filters.Add(new HandleErrorAttribute());
        }
    }
}
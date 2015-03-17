using System;
using System.Linq;
using System.Web.Mvc;
using System.IdentityModel.Services;
using Republic.Core.Interfaces;

namespace Rhml.Mms.Web.Controllers
{
    public class HomeController : AppController
    {

        public HomeController(IServiceLocator service)
            : base(service)
        {        }


        /// <summary> The current application's landing page.  This should be
        /// changed early on for a new application.
        /// </summary>
        /// <returns>The default entry view for the application</returns>
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}

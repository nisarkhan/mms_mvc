using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Republic.Core.Interfaces;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Rhml.Mms.Business.Interfaces;
using Rhml.Mms.Web.ViewModel;

namespace Rhml.Mms.Web.Controllers
{
    public class RealtorController : AppController
    {
        //
        // GET: /Realtor/

        private readonly IRealtorService RealtorService;


        [NonAction]
        private ActionResult SaveApplication(RealtorViewModel model)
        {
            if (model == null) return RedirectToAction("Index");
            if (ModelState.IsValid)
            {
                var realtorMaster = model.ToModel();
                RealtorService.SaveRealtor(realtorMaster);

                if (PopulateAndCheckBusinessState())
                {
                    return RedirectToAction("Index");
                }
            }

            return View("EditRealtor", model);

        }
        

        public RealtorController(IServiceLocator service,
            IRealtorService realtor)
            : base(service)
        {

            RealtorService = realtor;
        }
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult GetRealtors([DataSourceRequest] DataSourceRequest request)
        {
            var realtors = RealtorService.GetRealtors()
                .Select(rt => RealtorListViewModel.ToView(rt))
               .OrderBy(rt => rt.CompanyName);
            return Json(realtors.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateRealtor()
        {
            RealtorViewModel model = new RealtorViewModel
            {
                UserId = Guid.Empty,
                Email = "",
                Avator = "http://www.republicmortgage.com/wp-content/uploads/2013/02/avatar_female_gray_frame.png",
                CompanyName = "",
                FirstName = "",
                LastName = "",
                IsPreffered = false,
                Password = ""

            };

            return View("EditRealtor", model);
            
        }

        [HttpPost]
        public ActionResult EditApplication(RealtorViewModel model)
        {
            return SaveApplication(model);
            return null;
        }

        [HttpGet]
        public ActionResult EditRealtor(Guid Id)
        {
            
            Realtors_Master realtor = RealtorService.GetRealtor(Id);
            RealtorViewModel model = RealtorViewModel.ToView(realtor);

            return View("EditRealtor", model);
        }

        [HttpPost]
        public ActionResult EditRealtor(RealtorViewModel model)
        {

            if (model == null) return RedirectToAction("Index");
            if (ModelState.IsValid)
            {
                var realtor = model.ToModel();
                RealtorService.SaveRealtor(realtor);

                if (PopulateAndCheckBusinessState())
                {
                    return RedirectToAction("Index");
                }
            }

            return View("EditRealtor", model);
        }

        public ActionResult DeleteRealtor(Guid Id)
        {
            RealtorService.DeleteRealtor(Id);
            return RedirectToAction("Index");
        }

    }
}

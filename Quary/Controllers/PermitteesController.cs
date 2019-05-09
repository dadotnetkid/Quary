using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Repository;

namespace Quary.Controllers
{
    [Authorize]
    public class PermitteesController : Controller
    {
        // GET: Permittees
        private UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult PermitteeGridViewPartial()
        {
            var model = unitOfWork.PermitteesRepo.Get();
            return PartialView("_PermitteeGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PermitteeGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Permittees item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    unitOfWork.PermitteesRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.PermitteesRepo.Get();
            return PartialView("_PermitteeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PermitteeGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Permittees item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    unitOfWork.PermitteesRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.PermitteesRepo.Get();
            return PartialView("_PermitteeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PermitteeGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))] System.Int32 Id)
        {
           // var model = new object[0];
            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    unitOfWork.PermitteesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.PermitteesRepo.Get();
            return PartialView("_PermitteeGridViewPartial", model);
        }
    }
}
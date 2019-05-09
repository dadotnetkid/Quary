using System;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Models;
using Models.Repository;

namespace Quary.New.Controllers
{
    [Authorize]
    public class PermiteesController : Controller
    {
        // GET: Permitees
        private UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult PermiteeGridViewPartial()
        {
            var model = unitOfWork.PermiteesRepo.Get();
            return PartialView("_PermiteeGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PermiteeGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Permitees item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    unitOfWork.PermiteesRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.PermiteesRepo.Get();
            return PartialView("_PermiteeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PermiteeGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Permitees item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    unitOfWork.PermiteesRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.PermiteesRepo.Get();
            return PartialView("_PermiteeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PermiteeGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))] System.Int32 Id)
        {
           // var model = new object[0];
            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    unitOfWork.PermiteesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.PermiteesRepo.Get();
            return PartialView("_PermiteeGridViewPartial", model);
        }
    }
}
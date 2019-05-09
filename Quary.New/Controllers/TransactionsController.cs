using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Repository;

namespace Quary.New.Controllers
{
    public class TransactionsController : Controller
    {
        // GET: Transactions
        private UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            
            return View();
        }

        [ValidateInput(false)]
        public ActionResult TransactionsGridViewPartial()
        {
            var param = Request.Params["DXCallbackName"];
            var model = unitOfWork.TransactionsRepo.Get();
            return PartialView("_TransactionsGridViewPartial",model );
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionsGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Transactions item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_TransactionsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionsGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Transactions item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_TransactionsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionsGridViewPartialDelete(System.Int32 Id)
        {
            var model = new object[0];
            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_TransactionsGridViewPartial", model);
        }
    }
}
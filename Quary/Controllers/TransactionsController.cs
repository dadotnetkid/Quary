using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Models;
using Models.Repository;
using Models.ViewModels;

namespace Quary.Controllers
{
    [Authorize(Roles ="Administrator,Registrar,Cashier")]
    public class TransactionsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Transactions
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TransactionPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? id)
        {
            var model = unitOfWork.PermitteesRepo.Fetch(m => m.Id == id).OrderByDescending(m=>m.Id).FirstOrDefault();
            if (model != null)
            {
                model.RenewTransaction();
            }
            return PartialView(model);
        }

        public ActionResult cboPermitteePartial()
        {
            var model = unitOfWork.PermitteesRepo.Get().ToList();


            return PartialView("_cboPermitteePartial", model);
        }

        public ActionResult PermitteePartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? permitteeId)
        {
            ViewBag.actionName = Request.Params["actionName"];
           var model = unitOfWork.PermitteesRepo.Fetch(m => m.Id == permitteeId).OrderByDescending(m => m.Id).FirstOrDefault();
            if (model != null)
            {
                model.RenewTransaction();
            }
            return PartialView(model);
        }


        [HttpPost]
        public ActionResult RegisterPermitteePartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            Permittees item, [ModelBinder(typeof(DevExpressEditorsBinder))]
            Transactions transactions)
        {
            try
            {
                transactions.CreatedAt = DateTime.Now;
                transactions.ModifiedAt = DateTime.Now;
                item.Transactions.Add(transactions);
                unitOfWork.PermitteesRepo.Insert(item);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
            }
            var model = unitOfWork.PermitteesRepo.Find(m => m.Id == item.Id);
            return PartialView("TransactionPartial", model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult RenewPermitteePartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            Permittees permittees, [ModelBinder(typeof(DevExpressEditorsBinder))]
            Transactions transactions, [ModelBinder(typeof(DevExpressEditorsBinder))]
            int? permitteeId)
        {
            try
            {
                permittees.Id = permitteeId ?? 0;
                unitOfWork.TransactionsRepo.Insert(transactions);
                unitOfWork.PermitteesRepo.Update(permittees);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var model = unitOfWork.PermitteesRepo.Find(m => m.Id == permitteeId);
            return PartialView("TransactionPartial", model);
        }

        public ActionResult PermitteeInformationPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            int? permitteeId)
        {
            var model = unitOfWork.PermitteesRepo.Fetch(m => m.Id == permitteeId).OrderByDescending(m => m.Id).FirstOrDefault();
            if (model != null)
            {
                model.RenewTransaction();
            }
            return PartialView(model);
        }
        public ActionResult SAGAdvancePaymentPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int permitteeId)
        {
            ViewBag.PermitteeId = permitteeId;
            return PartialView();
        }
        public ActionResult AddSAGAdvancePaymentPartial([ModelBinder(typeof(DevExpressEditorsBinder))]Sags sags, [ModelBinder(typeof(DevExpressEditorsBinder))]int permitteeId)
        {
            
            var model = unitOfWork.PermitteesRepo.Fetch(m => m.Id == permitteeId).OrderByDescending(m => m.Id).FirstOrDefault();

            try
            {
                if (model != null)
                {
                    var transaction = model.Transactions.OrderByDescending(m => m.Id).FirstOrDefault();
                    if (transaction != null)
                    {
                        transaction.Sags.Add(sags);
                        unitOfWork.Save();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
            return PartialView("TransactionPartial", model);
        }
        #region Grid
        [ValidateInput(false)]
        public ActionResult TransactionGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            int? permitteeId)
        {
            var model = unitOfWork.TransactionsRepo.Get(m => m.PermitteeId == permitteeId);
            ViewBag.PermitteeId = permitteeId;
            return PartialView("_TransactionGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Transactions item)
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
            return PartialView("_TransactionGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Transactions item)
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
            return PartialView("_TransactionGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]System.Int32 Id, [ModelBinder(typeof(DevExpressEditorsBinder))]System.Int32 permitteeId)
        {
         //   var model = new object[0];
            if (Id >= 0)
            {
                try
                {
                    unitOfWork.TransactionsRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.TransactionsRepo.Get(m => m.PermitteeId == permitteeId);
            ViewBag.PermitteeId = permitteeId;
            return PartialView("_TransactionGridViewPartial", model);
        }


        #endregion
    }
}
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helpers;
using Microsoft.AspNet.Identity;
using Models.Repository;

namespace Quary.New.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {

        public string UserId => User.Identity.GetUserId();
        // GET: Transactions
        private UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {

            return View();
        }

        #region transaction-grid

        public ActionResult AddEditTransactionPartial([ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? permiteeId)
        {
            var model = unitOfWork.TransactionsRepo.Find(m => m.Id == transactionId);
            if (transactionId == null)
                transactionId = Guid.NewGuid().ToString();
            Debug.Write($"TransactionId {transactionId}");
            ViewBag.TransactionId = transactionId;//Guid.NewGuid().ToString();
            ViewBag.PermiteeId = model?.PermiteeId ?? permiteeId;
            var _p = (model?.PermiteeId ?? permiteeId);
            ViewBag.Quaries = unitOfWork.PermiteesRepo.Find(m => m.Id == _p)?._QuarySites;

            return PartialView(model);
        }

        [ValidateInput(false)]
        public ActionResult TransactionsGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? permiteeId)
        {
            ViewBag.TransactionId = transactionId;//Guid.NewGuid().ToString();
            ViewBag.PermiteeId = permiteeId;

            var model = unitOfWork.TransactionsRepo.Get();
            return PartialView("_TransactionsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionsGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId, [ModelBinder(typeof(DevExpressEditorsBinder))] Models.Transactions item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    item.TransactionDate = DateTime.Now;
                    var vehiclesCost = unitOfWork.TransactionVehiclesRepo.Fetch(m => m.TransactionId == transactionId).Sum(m => m.Cost);
                    var itemCost = unitOfWork.TransactionDetailsRepo.Fetch(m => m.TransactionId == transactionId).ToList()
                        .Sum(m => m.TotalCost);
                    var facilitiesCost = unitOfWork.TransactionFacilitiesRepo.Fetch(m => m.TransactionId == transactionId)
                        .Sum(m => m.Cost);
                    item.TransactionTotal = (vehiclesCost ?? 0) + (itemCost ?? 0) + (facilitiesCost ?? 0);
                    item.Id = transactionId;
                    item.EntryBy = UserId;
                    item.LastEditedBy = UserId;
                    item.TransactionDate = DateTime.Now;
                    unitOfWork.TransactionsRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.TransactionsRepo.Get(includeProperties: "Permitees,Permitees.PermiteeTypes");
            return PartialView("_TransactionsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionsGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId, [ModelBinder(typeof(DevExpressEditorsBinder))] Models.Transactions item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    var transaction = unitOfWork.TransactionsRepo.Find(m => m.Id == item.Id);
                    var vehiclesCost = unitOfWork.TransactionVehiclesRepo.Fetch(m => m.TransactionId == item.Id).Sum(m => m.Cost);
                    var itemCost = unitOfWork.TransactionDetailsRepo.Fetch(m => m.TransactionId == item.Id).ToList()
                        .Sum(m => m.TotalCost);
                    var facilitiesCost = unitOfWork.TransactionFacilitiesRepo.Fetch(m => m.TransactionId == item.Id)
                        .Sum(m => m.Cost);

                    transaction.TransactionTotal = (vehiclesCost ?? 0) + (itemCost ?? 0) + (facilitiesCost ?? 0);
                    transaction.LastEditedBy = UserId;
                    

               
                   
                   
                    unitOfWork.Save();
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.TransactionsRepo.Get(includeProperties: "Permitees,Permitees.PermiteeTypes");
            return PartialView("_TransactionsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionsGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]string Id)
        {
            //var model = new object[0];
            if (Id != null)
            {
                try
                {
                    unitOfWork.TransactionsRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.TransactionsRepo.Get(includeProperties: "Permitees,Permitees.PermiteeTypes");
            return PartialView("_TransactionsGridViewPartial", model);
        }



        #endregion

        #region transaction-detail
        public ActionResult AddEditTransactionDetailsPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? itemId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? transactionDetailId, [ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId)
        {
            var model = unitOfWork.TransactionDetailsRepo.Find(m => m.Id == transactionDetailId);
            ViewBag.TransactionId = transactionId;
            ViewBag.Items = unitOfWork.ItemsRepo.Find(m => m.Id == itemId);
            return PartialView(model);
        }


        [ValidateInput(false)]
        public ActionResult TransactionDetailsGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? itemId, [ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId)
        {
            var model = unitOfWork.TransactionDetailsRepo.Get(m => m.TransactionId == transactionId, includeProperties: "Items");
            ViewBag.TransactionId = transactionId;
            ViewBag.Items = unitOfWork.ItemsRepo.Find(m => m.Id == itemId);
            return PartialView("_TransactionDetailsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionDetailsGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.TransactionDetails item, [ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    item.EntryBy = UserId;
                    item.LastEditedBy = UserId;
                    unitOfWork.TransactionDetailsRepo.Insert(item);
                    unitOfWork.Save();
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.TransactionDetailsRepo.Get(m => m.TransactionId == transactionId, includeProperties: "Items");
            ViewBag.TransactionId = transactionId;
            return PartialView("_TransactionDetailsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionDetailsGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.TransactionDetails item, [ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.TransactionDetailsRepo.Update(item);
                    unitOfWork.Save();
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.TransactionDetailsRepo.Get(m => m.TransactionId == transactionId, includeProperties: "Items");
            ViewBag.TransactionId = transactionId;
            return PartialView("_TransactionDetailsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionDetailsGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id, [ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId)
        {

            if (Id >= 0)
            {
                try
                {
                    unitOfWork.TransactionDetailsRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.TransactionDetailsRepo.Get(m => m.TransactionId == transactionId, includeProperties: "Items");
            ViewBag.TransactionId = transactionId;
            return PartialView("_TransactionDetailsGridViewPartial", model);
        }


        #endregion

        public ActionResult PopupAddEditPermiteePartial()
        {
            return PartialView();
        }




        #region Transaction Vehicles

        public ActionResult AddEditTransactionVehiclesPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? vehicleId, [ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? transactionVehicleId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? permiteeId)
        {
            var model = unitOfWork.TransactionVehiclesRepo.Find(m => m.Id == transactionVehicleId);
            ViewBag.TransactionId = transactionId;
            ViewBag.PermiteeId = permiteeId;
            ViewBag.Vehicle = unitOfWork.VehiclesRepo.Find(m => m.Id == vehicleId);
            return PartialView(model);
        }

        [ValidateInput(false)]
        public ActionResult VehicleGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? vehicleId, [ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? permiteeId)
        {
            var model = unitOfWork.TransactionVehiclesRepo.Get(m => m.TransactionId == transactionId);
            ViewBag.TransactionId = transactionId;
            ViewBag.PermiteeId = permiteeId;
            ViewBag.Vehicle = unitOfWork.VehiclesRepo.Find(m => m.Id == vehicleId);
            return PartialView("_VehicleGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult VehicleGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.TransactionVehicles item, [ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? permiteeId)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    item.EntryBy = UserId;
                    item.EntryModifiedBy = UserId;
                    unitOfWork.TransactionVehiclesRepo.Insert(item);
                    unitOfWork.Save();
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.TransactionVehiclesRepo.Get(m => m.TransactionId == transactionId);
            ViewBag.TransactionId = transactionId;
            ViewBag.PermiteeId = permiteeId;
            return PartialView("_VehicleGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult VehicleGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.TransactionVehicles item, [ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? permiteeId)
        {
            //  var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    item.EntryModifiedBy = UserId;
                    unitOfWork.TransactionVehiclesRepo.Update(item);
                    unitOfWork.Save();
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.TransactionVehiclesRepo.Get(m => m.TransactionId == transactionId);
            ViewBag.TransactionId = transactionId;
            ViewBag.PermiteeId = permiteeId;
            return PartialView("_VehicleGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult VehicleGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))] int? Id, [ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? permiteeId)
        {
            // var model = new object[0];
            if (Id >= 0)
            {
                try
                {
                    unitOfWork.TransactionVehiclesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.TransactionVehiclesRepo.Get(m => m.TransactionId == transactionId);
            ViewBag.TransactionId = transactionId;
            ViewBag.PermiteeId = permiteeId;
            return PartialView("_VehicleGridViewPartial", model);
        }


        #endregion

        #region Transaction Facilities

        public PartialViewResult AddEditTransactionFacilitiesPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? facilitiesId, [ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? transactionFacilitiesId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? permiteeId)
        {
            var model = unitOfWork.TransactionFacilitiesRepo.Find(m => m.Id == transactionFacilitiesId);
            ViewBag.TransactionId = transactionId;
            ViewBag.PermiteeId = permiteeId;
            ViewBag.Facilities = unitOfWork.FacilitiesRepo.Get();
            ViewBag.facility = unitOfWork.FacilitiesRepo.Find(m => m.Id == facilitiesId);
            return PartialView(model);
        }

        [ValidateInput(false)]
        public ActionResult FacilitiesGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? permiteeId)
        {
            var model = unitOfWork.TransactionFacilitiesRepo.Get(m => m.TransactionId == transactionId, includeProperties: "Facilities");
            ViewBag.TransactionId = transactionId;
            ViewBag.PermiteeId = permiteeId;
            return PartialView("_FacilitiesGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult FacilitiesGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.TransactionFacilities item, [ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? permiteeId)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    item.EntryBy = UserId;
                    item.EntryModifiedBy = UserId;
                    unitOfWork.TransactionFacilitiesRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.TransactionFacilitiesRepo.Get(m => m.TransactionId == transactionId, includeProperties: "Facilities");
            ViewBag.TransactionId = transactionId;
            ViewBag.PermiteeId = permiteeId;
            return PartialView("_FacilitiesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult FacilitiesGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.TransactionFacilities item, [ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? permiteeId)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    item.EntryModifiedBy = UserId;
                    unitOfWork.TransactionFacilitiesRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.TransactionFacilitiesRepo.Get(m => m.TransactionId == transactionId, includeProperties: "Facilities");
            ViewBag.TransactionId = transactionId;
            ViewBag.PermiteeId = permiteeId;
            return PartialView("_FacilitiesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult FacilitiesGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int Id, [ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? permiteeId)
        {

            if (Id >= 0)
            {
                try
                {
                    unitOfWork.TransactionFacilitiesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.TransactionFacilitiesRepo.Get(m => m.TransactionId == transactionId, includeProperties: "Facilities");
            ViewBag.TransactionId = transactionId;
            ViewBag.PermiteeId = permiteeId;
            return PartialView("_FacilitiesGridViewPartial", model);
        }


        #endregion

        public ActionResult TransactionCallbackPanel([ModelBinder(typeof(DevExpressEditorsBinder))]string transactionId, [ModelBinder(typeof(DevExpressEditorsBinder))]int? permiteeId)
        {
            ViewBag.transactionId = transactionId;
            ViewBag.PermiteeId = permiteeId;

            var model = unitOfWork.TransactionsRepo.Get();
            return PartialView();
        }


    }
}
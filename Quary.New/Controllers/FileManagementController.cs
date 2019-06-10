using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Models;
using Models.Repository;
using Helpers;
namespace Quary.New.Controllers
{
    [Authorize]
    [RoutePrefix("file-management")]
    public class FileManagementController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: FileManagement
        public ActionResult Index()
        {
            return View();
        }
        [Route("transaction-items")]
        public ActionResult TransactionItems()
        {
            return View();
        }
        [Route("permitees")]
        public ActionResult Permitees()
        {
            return View();
        }
        [Route("permitee-types")]
        public ActionResult PermiteeTypes()
        {
            return View();
        }
        [Route("vehicles")]
        public ActionResult Vehicles()
        {
            return View();
        }
        [Route("vehicle-types")]
        public ActionResult VehicleTypes()
        {
            return View();
        }
        [Route("facilities")]
        public ActionResult Facilities()
        {
            return View();
        }
        [Route("quarries")]
        public ActionResult Quarries()
        {
            return View();
        }

        #region transaction-items

        public ActionResult AddEditTransactionItemPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? itemId)
        {
            var item = unitOfWork.ItemsRepo.Find(m => m.Id == itemId);
            return PartialView(item);
        }
        [ValidateInput(false)]
        public ActionResult TransactionItemsGridViewPartial()
        {
            var model = unitOfWork.ItemsRepo.Get();
            return PartialView("_TransactionItemsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionItemsGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Items item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    unitOfWork.ItemsRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;

                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;

            }

            var model = unitOfWork.ItemsRepo.Get();
            return PartialView("_TransactionItemsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionItemsGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Items item, [ModelBinder(typeof(DevExpressEditorsBinder))]Items oldModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.ItemsRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.ItemsRepo.Get();
            return PartialView("_TransactionItemsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TransactionItemsGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    unitOfWork.ItemsRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.ItemsRepo.Get();
            return PartialView("_TransactionItemsGridViewPartial", model);
        }


        #endregion

        #region permitee-types

        public ActionResult AddEditPermiteeTypePartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? permiteeTypeId)
        {
            var model = unitOfWork.PermiteeTypesRepo.Find(m => m.Id == permiteeTypeId);
            return PartialView(model);
        }
        [ValidateInput(false)]
        public ActionResult PermiteeTypeGridViewPartial()
        {
            var model = unitOfWork.PermiteeTypesRepo.Get();
            return PartialView("_PermiteeTypeGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PermiteeTypeGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.PermiteeTypes item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.PermiteeTypesRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }

            var model = unitOfWork.PermiteeTypesRepo.Get();
            return PartialView("_PermiteeTypeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PermiteeTypeGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.PermiteeTypes item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.PermiteeTypesRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.PermiteeTypesRepo.Get();
            return PartialView("_PermiteeTypeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PermiteeTypeGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    unitOfWork.PermiteeTypesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.PermiteeTypesRepo.Get();
            return PartialView("_PermiteeTypeGridViewPartial", model);
        }

        #endregion

        #region quarries
        [ValidateInput(false)]
        public ActionResult QuarriesGridViewPartial()
        {
            var model = unitOfWork.QuarriesRepo.Get();
            return PartialView("_QuarriesGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult QuarriesGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Quarries item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    item.LastEditedBy = User.Identity.GetUserId();
                    item.EntryBy = User.Identity.GetUserId();
                    unitOfWork.QuarriesRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }

            var model = unitOfWork.QuarriesRepo.Get();
            return PartialView("_QuarriesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult QuarriesGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Quarries item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    item.LastEditedBy = User.Identity.GetUserId();
                    unitOfWork.QuarriesRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }
            var model = unitOfWork.QuarriesRepo.Get();
            return PartialView("_QuarriesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult QuarriesGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    unitOfWork.QuarriesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.QuarriesRepo.Get();
            return PartialView("_QuarriesGridViewPartial", model);
        }

        public ActionResult AddEditQuarriesPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? quarriesId)
        {
            var model = unitOfWork.QuarriesRepo.Find(m => m.Id == quarriesId);
            return PartialView(model);
        }

        #endregion

        #region permitees
        [ValidateInput(false)]
        public ActionResult PermiteeGridViewPartial()
        {
            var model = unitOfWork.PermiteesRepo.Get();
            return PartialView("_PermiteeGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PermiteeGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Permitees item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    foreach (var i in item.QuarrySites)
                    {
                        var id = Convert.ToInt32(i);
                        item.Quarries.Add(unitOfWork.QuarriesRepo.Find(m=>m.Id==id));
                    }
                    item.EntryBy = User.Identity.GetUserId();
                    item.LastEditedBy = User.Identity.GetUserId();
                    unitOfWork.PermiteesRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }

            var model = unitOfWork.PermiteesRepo.Get();
            return PartialView("_PermiteeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PermiteeGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Permitees item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    
                    unitOfWork.PermiteesRepo.Update(item);
                    unitOfWork.Save();
                    var permitees = unitOfWork.PermiteesRepo.Find(m => m.Id == item.Id,includeProperties:"Quarries");
                    permitees.Quarries.Clear();
                    foreach(var i in item.QuarrySites)
                    {
                        var id = Convert.ToInt32(i);
                        permitees.Quarries.Add(unitOfWork.QuarriesRepo.Find(m => m.Id == id));
                    }
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }
            var model = unitOfWork.PermiteesRepo.Get(includeProperties: "Quarries");
            return PartialView("_PermiteeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PermiteeGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {

            if (Id >= 0)
            {
                try
                {
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

        public ActionResult AddEditPermiteePartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? permiteeId)
        {
            var model = unitOfWork.PermiteesRepo.Find(m => m.Id == permiteeId);
            return PartialView(model);
        }

        public PartialViewResult TokenBoxQuarriesInPermiteePartial([ModelBinder(typeof(DevExpressEditorsBinder))] int? permiteeId)
        {
            ViewBag.PermiteesQuarry = unitOfWork.QuarriesRepo.Fetch(m => m.Permitees.Any(x => x.Id == permiteeId)).ToList();
            var model = unitOfWork.QuarriesRepo.Get();
            return PartialView(model);
        }

        
        #endregion

        #region vehicle-types

        [ValidateInput(false)]
        public ActionResult VehicleTypesGridViewPartial()
        {
            var model = unitOfWork.VehicleTypesRepo.Get();
            return PartialView("_VehicleTypesGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult VehicleTypesGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.VehicleTypes item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.VehicleTypesRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }
            var model = unitOfWork.VehicleTypesRepo.Get();
            return PartialView("_VehicleTypesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult VehicleTypesGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.VehicleTypes item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                   
                    unitOfWork.VehicleTypesRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }
            var model = unitOfWork.VehicleTypesRepo.Get();
            return PartialView("_VehicleTypesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult VehicleTypesGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    unitOfWork.VehicleTypesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.VehicleTypesRepo.Get();
            return PartialView("_VehicleTypesGridViewPartial", model);
        }

        public PartialViewResult AddEditVehicleTypePartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? vehicleTypeId)
        {
            var model = unitOfWork.VehicleTypesRepo.Find(m => m.Id == vehicleTypeId);
            return PartialView(model);
        }


        #endregion

        #region vehicles

        [ValidateInput(false)]
        public ActionResult VehiclesGridViewPartial()
        {
            var model = unitOfWork.VehiclesRepo.Get().ToList();
            return PartialView("_VehiclesGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult VehiclesGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Vehicles item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.VehiclesRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }

            var model = unitOfWork.VehiclesRepo.Get(includeProperties: "VehicleTypes,Permitees");
            return PartialView("_VehiclesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult VehiclesGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Vehicles item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.VehiclesRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }
            var model = unitOfWork.VehiclesRepo.Get(includeProperties: "VehicleTypes,Permitees");
            return PartialView("_VehiclesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult VehiclesGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))] int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    unitOfWork.VehiclesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.VehiclesRepo.Get(includeProperties: "VehicleTypes,Permitees");
            return PartialView("_VehiclesGridViewPartial", model);
        }

        public PartialViewResult AddEditVehiclePartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? vehicleId)
        {
            var model = unitOfWork.VehiclesRepo.Find(m => m.Id == vehicleId);
            return PartialView(model);
        }

        #endregion

        #region facilities

        [ValidateInput(false)]
        public ActionResult FacilitiesGridViewPartial()
        {
            var model = unitOfWork.FacilitiesRepo.Get();
            return PartialView("_FacilitiesGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult FacilitiesGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Facilities item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    unitOfWork.FacilitiesRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }
            var model = unitOfWork.FacilitiesRepo.Get();
            return PartialView("_FacilitiesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult FacilitiesGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Facilities item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    unitOfWork.FacilitiesRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                ViewData["Model"] = item;
            }
            var model = unitOfWork.FacilitiesRepo.Get();
            return PartialView("_FacilitiesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult FacilitiesGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    unitOfWork.FacilitiesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.FacilitiesRepo.Get();
            return PartialView("_FacilitiesGridViewPartial", model);
        }

        public PartialViewResult AddEditFacilitiesPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? facilitiesId)
        {
            var model = unitOfWork.FacilitiesRepo.Find(m => m.Id == facilitiesId);
            return PartialView(model);
        }

        #endregion
    }
}
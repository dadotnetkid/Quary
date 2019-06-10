using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Helpers;
using Models;
using Models.ControllerHelpers;
using Models.Repository;

namespace Quary.New.Controllers
{

    public class MaintenanceController : IdentityController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Maintenance
        public ActionResult Index()
        {
            return View();
        }

        #region Users


        public ActionResult Users()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult UsersGridViewPartial()
        {
            var model = unitOfWork.UsersRepo.Get();
            return PartialView("_UsersGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> UsersGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Users item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    item.Id = Guid.NewGuid().ToString();
                    item.UserName = item.Email.Split('@')[0];
                    var userResult = await UserManager.CreateAsync(item, item.Password);
                    if (userResult.Succeeded)
                    {

                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["Model"] = item;
                ViewData["EditError"] = "Please, correct all errors.";
            }

            var model = unitOfWork.UsersRepo.Get();

            return PartialView("_UsersGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> UsersGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Users item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var user = unitOfWork.UsersRepo.Find(m => m.Id == item.Id);
                    user.FirstName = item.FirstName;
                    user.MiddleName = item.MiddleName;
                    user.LastName = item.LastName;
                    user.Email = item.Email;
                    user.UserName = item.Email.Split('@')[0];
                    if (item.Password != null)
                    {
                        var res = await UserManager.ChangePasswordAsync(user, item.Password);
                        if (res.Succeeded)
                            Debug.Write(res.Succeeded);
                    }

                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.UsersRepo.Get();

            return PartialView("_UsersGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> UsersGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]string Id)
        {
            if (Id != null)
            {
                try
                {
                    await UserManager.DeleteAsync(await UserManager.FindByIdAsync(Id));
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.UsersRepo.Get();
            return PartialView("_UsersGridViewPartial", model);
        }

        public PartialViewResult AddEditUserPartial([ModelBinder(typeof(DevExpressEditorsBinder))]string userId)
        {
            var model = unitOfWork.UsersRepo.Find(m => m.Id == userId);
            return PartialView(model);
        }

        #endregion

        #region UsersInController

        public PartialViewResult CboControllersPartial([ModelBinder(typeof(DevExpressEditorsBinder))]string controllerName)
        {
            ViewBag.ControllerName = controllerName;
            var model = new ControllerActionHelper().ControllerNames().ToList();
            return PartialView(model);
        }
        public PartialViewResult CboActionsPartial([ModelBinder(typeof(DevExpressEditorsBinder))]string actionName, [ModelBinder(typeof(DevExpressEditorsBinder))]string controllerName)
        {
            ViewBag.ActionName = actionName;
            ViewBag.ControllerName = controllerName;
            var model = new ControllerActionHelper().ActionNames(controllerName);
            return PartialView(model);
        }

        public PartialViewResult TokenBoxUsersPartials([ModelBinder(typeof(DevExpressEditorsBinder))]int? controllersActionsId)
        {
            ViewBag.Users = unitOfWork.UsersRepo.Get(x => x.ControllersActions.Any(m => m.Id == controllersActionsId)).Select(x => x.FullName);
            return PartialView(unitOfWork.UsersRepo.Get());
        }
        public ActionResult AddEditControllersActionsPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? controllersActionsId)
        {
            var model = unitOfWork.ControllersActionsRepo.Find(m => m.Id == controllersActionsId);
            return PartialView(model);
        }
        public ActionResult Controllers()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ControllersGridView()
        {
            var model = unitOfWork.ControllersActionsRepo.Get();
            return PartialView("_ControllersGridView", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ControllersGridViewAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.ControllersActions item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model

                    foreach (var i in item.UserId)
                    {
                        item.Users.Add(unitOfWork.UsersRepo.Find(m => m.Id == i));
                    }
                    unitOfWork.ControllersActionsRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.ControllersActionsRepo.Get();
            return PartialView("_ControllersGridView", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ControllersGridViewUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.ControllersActions item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var res = unitOfWork.ControllersActionsRepo.Find(m => m.Id == item.Id);
                    foreach (var i in res.Users)
                        res.Users.Remove(i);
                    foreach (var i in res.UserId)
                        res.Users.Add(unitOfWork.UsersRepo.Find(m => m.Id == i));
                    res.Controller = item.Controller;
                    res.Action = item.Action;
                    unitOfWork.Save();



                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.ControllersActionsRepo.Get();
            return PartialView("_ControllersGridView", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ControllersGridViewDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    unitOfWork.ControllersActionsRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.ControllersActionsRepo.Get();
            return PartialView("_ControllersGridView", model);
        }
        #endregion


    }
}
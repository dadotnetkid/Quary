using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Models;
using Models.Controllers;
using Models.Repository;
using Models.ViewModels;

namespace Quary.Controllers
{
    public class MemberController : IdentityController
    {
        // GET: Member
        private UnitOfWork unitOfWork = new UnitOfWork();
       

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.None)]
        public async Task<ActionResult> Login([ModelBinder(typeof(DevExpressEditorsBinder))]LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var user = UserManager.FindByEmail(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("UserName", "Username not exist");
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(user?.UserName, model.Password, model.RememberMe ?? false, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("UserName", "Invalid Email.");
                    ModelState.AddModelError("Password", "Invalid Password.");
                    return View(model);
            }
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var user = new Users() { Id = Guid.NewGuid().ToString(), UserName = model.Email, Email = model.Email, FirstName = model.FirstName, MiddleName = model.MiddleName, LastName = model.LastName };
            var res = await UserManager.CreateAsync(user, model.Password);
            if (res.Succeeded)
            {
                UserManager.AddToRole(user.Id, "");
            }
            return RedirectToAction("Login");
        }

        public ActionResult RegisterPartial(RegistrationType registrationType)
        {
            return PartialView(new RegisterViewModel() { RegistrationType = registrationType });
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Transactions");
        }
        [Route("log-off")]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Transactions");
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}
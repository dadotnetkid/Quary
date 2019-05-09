using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Repository;

namespace Quary.Controllers
{
    [Authorize(Users = string.Join(",", new UnitOfWork().UsersRepo.Get().Select(x => x.Email))) ]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var roles = new Models.UserRoles();
            roles.Id = Guid.NewGuid().ToString();
            roles.Name = "";
            ModelDb db = new ModelDb();
            db.UserRoles.Add(roles);
            db.SaveChanges();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quary.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }
        [Route("transaction-error")]
        public ActionResult TransactionsError([ModelBinder(typeof(DevExpressEditorsBinder))]
            string error)
        {
            ViewBag.Error = error;
            return PartialView();
        }
    }
}
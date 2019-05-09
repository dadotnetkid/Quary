using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace Quary.New.Controllers
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
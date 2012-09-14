using System.Web.Mvc;
using System.Collections.Specialized;
using System.Configuration;

namespace DSH.Main.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            NameValueCollection appSettings
                 = ConfigurationManager.AppSettings;

            ViewData["ApplicationTitle"] =
                  appSettings["ApplicationTitle"];
            return View();
        }

    }
}


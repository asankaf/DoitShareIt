using System.Web.Mvc;
using System.Collections.Specialized;
using System.Configuration;
using DSH.Main.Web.Models;

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
            if (Session["userurl"] != null)
            {
                return View();
            }
            else
            {
                //return View("Signin");
                return View();
            }
        }

        public ActionResult Signin()
        {


            return View();
        }


        public ActionResult Login()
        {


            return View("index");
        }


        [HttpPost]
        public ActionResult Login(LoginData login)
        {



            string id = login.id;
            string name = login.name;
            string lname = login.lname;
            string url = login.url;
            string image = login.image;

            Session["userurl"] = url;
            Session["userfname"] = name;
            Session["userpic"] = image;


            return Json(login);






        }

    }
}


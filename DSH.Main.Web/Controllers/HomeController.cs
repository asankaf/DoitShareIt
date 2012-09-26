using System.Web.Mvc;
using System.Collections.Specialized;
using System.Configuration;
using DSH.Main.Web.Models;
using DSH.Access.DataModels;
using DSH.DataAccess.Services;


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
                return View("Signin");
                 // return View();
            }
        }

        public ActionResult Signin()
        {


            return View();
        }


     /*   public ActionResult Login()
        {


            return View("index");
        }*/


        [HttpPost]
        public ActionResult Login(Users login)
        {
            
            //string id = login.Id;
            //string name = login.;
            //string lname = login.lname;
            //string url = login.url;
            //string image = login.image;

            Session["id"] = login.UserUniqueid;
            Session["userurl"] = login.PublicProfileUrl;
            Session["userfname"] = login.DisplayName;
            Session["userpic"] = login.PicLocation;

            
            UserDataAccess current = new UserDataAccess();
            if (current.GetUserInfo(Session["id"].ToString()) == null) {
                current.InsertUserInfo(login);
            }


            


            return Json(login);

        }

        public ActionResult Logout()
        {
            Session.RemoveAll();

            return RedirectToAction("Index");

        }

        [HttpPost]
        public JsonResult Userprofile()
        {

            Users me = new Users();
            UserDataAccess current = new UserDataAccess();
            me = current.GetUserInfo(Session["id"].ToString());

            //me.DisplayName = "Kitty";
            //me.PicLocation = "Content\test.jpg";
            //me.Reputation = 15;

            

            return Json(me);
        }

    }
}


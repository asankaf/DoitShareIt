using System.Web.Mvc;
using System.Collections.Specialized;
using System.Configuration;
using DSH.Access.DataModels;
using DSH.DataAccess.Services;
using System.Collections.Generic;


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
                //return View();
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

            Session["id"] = login.UserUniqueid;
            Session["userurl"] = login.PublicProfileUrl;
            Session["userfname"] = login.DisplayName;
            Session["userpic"] = login.PicLocation;

            
            UserDataAccess current = new UserDataAccess();
            var thisUser = current.GetUserInfo(Session["id"].ToString());
            if ( thisUser == null) {
                current.InsertUserInfo(login);
            }

            return Json(login);


        }

        public ActionResult Logout()
        {
            Session.RemoveAll();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public JsonResult Searchuser(string searchText)
        {

            List<Users> searchUser = new List<Users>();
            UserDataAccess current = new UserDataAccess();
            searchUser = current.MatchUser(searchText, 5);

            return Json(searchUser, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Userprofile()
        {

            Users me = new Users();
            var current = new UserDataAccess();
            me = current.GetUserInfo(Session["id"].ToString());
            //Session["id"] = "qou7GONQq7";
            //me = current.GetUserInfo("qou7GONQq7");
            
            return Json(me);
        }

        [HttpPost]
        public ActionResult Viewuser()
        {

            return View("Index");
        }

    }
}


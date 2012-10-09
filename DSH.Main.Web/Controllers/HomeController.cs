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

            #region

            /*          fake data to login for testing with out a internet connnectoin 
            Session["id"] = "szaF9K6odR";
            Session["userurl"] = "http://www.linkedin.com/in/roshandhananajaya";
            Session["userfname"] = "Roshan Dhananajaya";
            Session["userpic"] = "its getting it from the database coz im not  a new user";
            */

            #endregion

            var current = new UserDataAccess();
            Users thisUser = current.GetUserInfo(Session["id"].ToString());
            if (thisUser == null)
            {
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
        public JsonResult Userprofile(string id)
        {
            Users me = new Users();
            var current = new UserDataAccess();
            

                me = (id == null) ? current.GetUserInfo(Session["id"].ToString()) : current.GetUserInfo(id);
           
            
            return Json(me);
        }

        [HttpPost]
        public ActionResult Viewuser()
        {

            return View("Index");
        }

    }
}


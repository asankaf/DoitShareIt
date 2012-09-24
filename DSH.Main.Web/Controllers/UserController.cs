using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSH.Access.DataModels;
using DSH.DataAccess.Services;






namespace DSH.Main.Web.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        [HttpGet]
        public ActionResult Index()
        {
            return Json(new
                            {
                                UserSummery = "User summery Object goes here" 
                                // todo: creat a user summery object and place it here

                            }, JsonRequestBehavior.AllowGet);
        }


/*     CAN'T create a user with a HttpGet ;; we are not allowing it */

        // GET: /User/Create

        public ActionResult Create()
        {
            return Json(new
                            {
                                Create = "Create"
                            }, JsonRequestBehavior.AllowGet);
        } 

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(Users user)
        {
            try
            {
                // TODO: Creat new user in database

                return Json(new
                                {
                                    // todo: after creating new user in database return something from here

                                }, JsonRequestBehavior.AllowGet );
            }
            catch
            {
                // return a "Coudn't create new user" message
                return View();
            }
        }


        [HttpGet]
        public ActionResult UserInfo(string id)
        {
            // todo: get the user info using the *id* and return the approprieate userInfo json object

            var userDataAccess = new UserDataAccess();
            Users users = userDataAccess.GetUserInfo(id);


            return Json(new
                            {
                                // todo: code code code .............
                                id = id,
                                user_info = users
                            }, JsonRequestBehavior.AllowGet);
        }
    }
}

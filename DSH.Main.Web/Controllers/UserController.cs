using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            var userDataAccess = new UserDataAccess();
            var userId = (string)Session["id"];

            var userInfo = userDataAccess.GetUserInfo(userId);



            return Json(new
            {
                Reputation = userInfo.Reputation,
                CreationDate = userInfo.CreationDate,
                DisplayName = userInfo.DisplayName,
                Views = userInfo.Views,
                Upvotes = userInfo.Upvotes,
                Downvotes = userInfo.Downvotes,
                PublicProfileUrl = userInfo.PublicProfileUrl
            }, JsonRequestBehavior.AllowGet);
        }

        /*

        /*     CAN'T create a user with a HttpGet ;; we are not allowing it #1#

                // GET: /User/Create

                public ActionResult Create()
                {
                    return Json(new
                                    {
                                        Create = "Create"
                                    }, JsonRequestBehavior.AllowGet);
                } 
        */

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(Users user)
        {

            

            try
            {
                // Creat new user in database

                // todo: validate

                // putting new user to database with linq
                var userDataAccess = new UserDataAccess();
                userDataAccess.InsertUserInfo(user);

                return Json(new
                {
                    //  after creating new user in database return something from here

                }, JsonRequestBehavior.AllowGet);
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
                user_info = users
            }, JsonRequestBehavior.AllowGet);
        }
    }
}

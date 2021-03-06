﻿using System;
using System.Web.Mvc;
using DSH.Access.DataModels;
using DSH.Access.FrequentUsers.Model;
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
            var userId = (string) Session["id"];

            Users userInfo = userDataAccess.GetUserInfo(userId);


            return Json(new
                            {
                                userInfo.Reputation,
                                userInfo.CreationDate,
                                userInfo.DisplayName,
                                userInfo.Views,
                                userInfo.Upvotes,
                                userInfo.Downvotes,
                                userInfo.PublicProfileUrl
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

        [HttpGet]
        public ActionResult GetUser(int id)
        {
            try
            {
                var userDataAccess = new UserDataAccess();
                Users user = userDataAccess.GetUser(id);

                return Json(new
                                {
                                    Status = "SUCCESS",
                                    Result = Json(user)
                                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new
                                {
                                    Status = "FAILED",
                                    Result = ""
                                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetUserPicUrl(int id)
        {
            try
            {
                var userDataAccess = new UserDataAccess();
                string url = userDataAccess.GetUserPicUrl(id);

                return Json(new
                                {
                                    Status = "SUCCESS",
                                    Result = url
                                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new
                                {
                                    Status = "FAILED",
                                    Result = ""
                                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult FrequentUser()
        {
            var userDataAccess = new UserDataAccess();
            var userId = (string) Session["id"];

            Users thisUserInfo = userDataAccess.GetUserInfo(userId);

            var frequentUserList = (new UserFrequency()).FrequentUsers(thisUserInfo.Id);



            


//          List<UserFrequency> ResultList = userDataAccess.


            return Json(new
                            {
                                Status = "SUCCESS",
                                Result = frequentUserList
                            }, JsonRequestBehavior.AllowGet);
        }
    }
}
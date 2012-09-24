using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSH.Access.UserAccess.Model;





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


/*     CAN'T create a user with a HttpGet ;; we are not allowing it
 * //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        } */

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

                                }, JsonRequestBehavior.DenyGet );
            }
            catch
            {
                // return a "Coudn't create new user" message
                return View();
            }
        }
    }
}

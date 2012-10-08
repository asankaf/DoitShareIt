using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSH.Main.Web.Models;

namespace DSH.Main.Web.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(LoginData login)
        {
            string id = login.id;
            string name = login.name;


            return Json(login);
        }


    }
}

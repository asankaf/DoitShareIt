using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSH.DataAccess;
using DSH.Main.Web.RESTComponents.Controller;

namespace DSH.Main.Web.Controllers
{
    public class RESTController : RestfulControllerBase
    {
        //
        // GET: /REST/
        [HttpGet]
        public ActionResult Index()
        {
            return Json(new
                            {
                                Request = "GET"
                            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Index(Post post)
        {
            return Json(post);
        }
    }
}

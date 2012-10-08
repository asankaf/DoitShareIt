using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSH.DataAccess;
using DSH.Main.Web.Models;
using DSH.Main.Web.Repositories;
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
                                Request = "GET",
                                Response = "Nothing"
                            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Index(Post post)
        {
            return Json(post);
        }

        /***************************************************************************************************/
        [HttpGet]
        public ActionResult GetEmployee()
        {
                EmployeesRepository repository = new EmployeesRepository();
                repository.Initialize();
                return Json(repository.GetEmployees(), JsonRequestBehavior.AllowGet);
        }
    }
}

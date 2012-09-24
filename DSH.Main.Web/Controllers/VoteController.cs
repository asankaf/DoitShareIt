using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSH.Main.Web.Controllers
{
    public class VoteController : Controller
    {
        //
        // GET: /Vote/
        [HttpGet]
        public ActionResult Index()
        {
            return Json(new
            {
                Status = "FAILED",
                Result = "Not Implemented Operation"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UpVote(int postId)
        {
            return Json(new
            {
                Status = "FAILED",
                Result = "Operation Not Implemented Yet"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DownVote(int postId)
        {
            return Json(new
            {
                Status = "FAILED",
                Result = "Operation Not Implemented Yet"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetScore(int postId)
        {
            return Json(new
            {
                Status = "FAILED",
                Result = "Operation Not Implemented Yet"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetReputation(int userId)
        {
            return Json(new
            {
                Status = "FAILED",
                Result = "Operation Not Implemented Yet"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}

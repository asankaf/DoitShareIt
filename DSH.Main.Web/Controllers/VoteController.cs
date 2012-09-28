using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSH.AccountingEngine;

namespace DSH.Main.Web.Controllers
{
    public class VoteController : Controller
    {
        private readonly AccountingService _accountingService;

        public VoteController()
        {
            _accountingService = new AccountingService();
        }

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
        public ActionResult UpVotePost(int postId)
        {
            try
            {
                var postScore = _accountingService.UpVotePost(postId,(string) Session["id"]);
                return Json(new
                {
                    Status = "SUCCESS",
                    Result = postScore
                }, JsonRequestBehavior.AllowGet);
            }catch(Exception e)
            {
                return Json(new
                {
                    Status = "FAILED",
                    Result = e.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult DownVotePost(int postId)
        {
            try
            {
                var postScore = _accountingService.DownVotePost(postId,(string) Session["id"]);
                return Json(new
                {
                    Status = "SUCCESS",
                    Result = postScore
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    Status = "FAILED",
                    Result = e.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult UpVoteComment(int commentId)
        {
            try
            {
                var commentScore = _accountingService.UpVoteComment(commentId,(string) Session["id"]);
                return Json(new
                {
                    Status = "SUCCESS",
                    Result = commentScore
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    Status = "FAILED",
                    Result = e.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpGet]
        //public ActionResult GetScore(int postId)
        //{
        //    return Json(new
        //    {
        //        Status = "FAILED",
        //        Result = "Operation Not Implemented Yet"
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult GetReputation(int userId)
        //{
        //    return Json(new
        //    {
        //        Status = "FAILED",
        //        Result = "Operation Not Implemented Yet"
        //    }, JsonRequestBehavior.AllowGet);
        //}
    }
}

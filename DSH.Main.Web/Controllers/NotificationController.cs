using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSH.DataAccess.Services;

namespace DSH.Main.Web.Controllers
{
    public class NotificationController : Controller
    {

        private readonly NotificationDataAccess _notificationDataAccess;
        private readonly UserDataAccess _userDataAccess;

        public NotificationController()
        {
            _notificationDataAccess = new NotificationDataAccess();
            _userDataAccess = new UserDataAccess();
        }

        //
        // GET: /Notification/

        public ActionResult Index()
        {
            try
            {
                var recipientId = _userDataAccess.GetUserInfo((string)Session["id"]).Id;
                var notifications = _notificationDataAccess.GetUnreadNotifications(recipientId);
                //Session[postType+"LastCheckoutTime"] = DateTime.Now.ToString();
                Session["CheckedoutNotificationCount"] = notifications.ToArray().Length;
                return Json(new
                {
                    Status = "SUCCESS",
                    Result = Json(notifications)
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
        public ActionResult GetNotifications()
        {
            try
            {
                var recipientId = _userDataAccess.GetUserInfo((string) Session["id"]).Id;  
                var notifications = _notificationDataAccess.GetNotifications(recipientId, 10);
                Session["CheckedoutNotificationCount"] = notifications.ToArray().Length;
                return Json(new
                {
                    Status = "SUCCESS",
                    Result = Json(notifications)
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
        public ActionResult GetMoreNotifications()
        {
            try
            {
                var recipientId = _userDataAccess.GetUserInfo((string)Session["id"]).Id;  
                var startIndex = (int)Session["CheckedoutNotificationCount"];
                var notifications = _notificationDataAccess.GetMoreNotifications(recipientId,startIndex,10);
                Session["CheckedoutNotificationCount"] = (int)Session["CheckedoutNotificationCount"] + notifications.ToArray().Length;

                return Json(new
                {
                    Status = "SUCCESS",
                    Result = Json(notifications)
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
        public ActionResult MarkNotificationRead(int notificationId)
        {
            try
            {
                var recipientId = _userDataAccess.GetUserInfo((string)Session["id"]).Id;
                _notificationDataAccess.MarkNotificationRead(notificationId, recipientId);
                return Json(new
                {
                    Status = "SUCCESS",
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

    }
}

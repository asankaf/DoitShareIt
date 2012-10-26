using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DSH.Access.PostAccess.Model;
using DSH.Main.Web.Controllers.AsyncHandlers;
using Microsoft.Web.Mvc;
using DSH.DataAccess.Services;

namespace DSH.Main.Web.Controllers
{
    [ControllerSessionState(ControllerSessionState.ReadOnly)]
    public class NotificationCheckingController : AsyncController
    {
        //
        // GET: /NotificationAsync/

        public void IndexAsync()
        {
            AsyncManager.OutstandingOperations.Increment();
            var notificationHandler = new NotificationAsyncHandler();
            var userDataAccess = new UserDataAccess();
            var currentUserId = userDataAccess.GetUserInfo((string)Session["id"]).Id;
            notificationHandler.CheckForNotificationAsync((notifications) =>
                                                              {
                                                                  AsyncManager.Parameters["notifications"] = notifications;
                                                                  AsyncManager.OutstandingOperations.Decrement();
                                                              },currentUserId);
        }

        public ActionResult IndexCompleted(List<Access.DataModels.Notification> notifications )
        {
            return Json(new
            {
                Status = "SUCCESS",
                Result = Json(AsyncManager.Parameters["notifications"])
            }, JsonRequestBehavior.AllowGet);
        }

    }
}

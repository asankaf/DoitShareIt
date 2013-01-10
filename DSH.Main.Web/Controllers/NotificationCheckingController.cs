using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DSH.Access.DataModels;
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
            var userSessionId = (string) Session["id"];
            if (userSessionId != null)
            {
                Users currentUser = userDataAccess.GetUserInfo(userSessionId);
                var currentUserId = currentUser.Id;
                notificationHandler.CheckForNotificationAsync((notifications) =>
                                                                  {
                                                                      AsyncManager.Parameters["notifications"] =
                                                                          notifications;
                                                                      AsyncManager.OutstandingOperations.Decrement();
                                                                  }, currentUserId);
            }
        }

        public ActionResult IndexCompleted(List<Access.DataModels.Notification> notifications)
        {
            if (notifications.Any())
            {
                return Json(new
                                {
                                    Status = "SUCCESS",
                                    Result = Json(AsyncManager.Parameters["notifications"])
                                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                                {
                                    Status = "FAILED"
                                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
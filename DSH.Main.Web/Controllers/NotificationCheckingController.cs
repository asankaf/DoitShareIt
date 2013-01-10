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
            if (userSessionId != null) // used to get a null reference exception here
                // to avoid that made this change 
                // I think the course of the null reference is there was no session data in the call to this func.
            {
                Users currentUser = userDataAccess.GetUserInfo(userSessionId);
                int currentUserId = currentUser.Id;
                notificationHandler.CheckForNotificationAsync((notifications) =>
                                                                  {
                                                                      AsyncManager.Parameters["notifications"] =
                                                                          notifications;
                                                                      AsyncManager.OutstandingOperations.Decrement();
                                                                  }, currentUserId);
            }
            else
            {
                // to-do: send a message that its not successful or something
                // other wise it will show 500 (Internal Server Error)
                //
            }

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

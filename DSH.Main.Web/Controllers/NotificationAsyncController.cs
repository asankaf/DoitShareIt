using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DSH.Main.Web.Controllers.AsyncHandlers;
using Microsoft.Web.Mvc;

namespace DSH.Main.Web.Controllers
{
    [ControllerSessionState(ControllerSessionState.Disabled)]
    public class NotificationController : AsyncController
    {
        //
        // GET: /NotificationAsync/

        public void IndexAsync()
        {
            AsyncManager.OutstandingOperations.Increment();
            var notificationHandler = new NotificationAsyncHandler();
            notificationHandler.CheckForNotificationAsync(() =>
                                                              {
                                                                  AsyncManager.OutstandingOperations.Decrement();
                                                              });
        }

        public ActionResult IndexCompleted()
        {
            return Json(new
            {
                Status = "FAILED",
                Result = ""
            }, JsonRequestBehavior.AllowGet);
        }

    }
}

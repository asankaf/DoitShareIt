using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using DSH.Access.PostAccess.Model;
using DSH.DataAccess.Services;

namespace DSH.Main.Web.Controllers.AsyncHandlers
{
    public class NotificationAsyncHandler
    {
        public delegate void CheckForNotificationResponse(List<Access.DataModels.Notification> notifications);

        public IAsyncResult CheckForNotificationAsync(CheckForNotificationResponse resp,int currentUserId)
        {
            return new MyAsyncResult(resp,currentUserId);
        }

        private class MyAsyncResult : IAsyncResult
        {
            private readonly CheckForNotificationResponse _mResp;

            public MyAsyncResult(CheckForNotificationResponse resp,int currentUserId)
            {
                _mResp = resp;
                var thread = new Thread(new ThreadStart(() =>
                                                               {
                                                                   var notificationDataAccees = new NotificationDataAccess();
                                                                   List<DSH.Access.DataModels.Notification>
                                                                       notifications;
                                                                   while(true)
                                                                   {
                                                                       notifications =
                                                                           notificationDataAccees.GetUnreadNotifications(currentUserId);
                                                                       if (notifications.Any()) break;
                                                                       Thread.Sleep(30000);                                                                   
                                                                   }
                                                                   _mResp(notifications);
                                                               }));
                thread.Start();
            }

            public bool IsCompleted { get; private set; }
            public WaitHandle AsyncWaitHandle { get; private set; }
            public object AsyncState { get; private set; }
            public bool CompletedSynchronously { get; private set; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace DSH.Main.Web.Controllers.AsyncHandlers
{
    public class NotificationAsyncHandler
    {
        public delegate void CheckForNotificationResponse();

        public IAsyncResult CheckForNotificationAsync(CheckForNotificationResponse resp)
        {
            return new MyAsyncResult(resp);
        }

        private class MyAsyncResult : IAsyncResult
        {
            private readonly CheckForNotificationResponse _mResp;

            public MyAsyncResult(CheckForNotificationResponse resp)
            {
                _mResp = resp;
                var thread = new Thread(new ThreadStart(() =>
                                                               {
                                                                   Thread.Sleep(30000);
                                                                   _mResp();
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
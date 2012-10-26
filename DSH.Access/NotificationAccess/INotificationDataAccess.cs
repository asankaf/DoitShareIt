using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSH.Access.DataModels;

namespace DSH.Access.PostAccess.Model
{
    public interface INotificationDataAccess
    {
        List<Notification> GetNotifications(int recipientId);
        void MarkNotificationRead(int notificationId, int recipientId);
        Notification CreateNewNotification(Notification notification);
    }
}

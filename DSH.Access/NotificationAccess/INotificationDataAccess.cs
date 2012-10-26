using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSH.Access.DataModels;

namespace DSH.Access.PostAccess.Model
{
    public interface INotificationDataAccess
    {
        List<Notification> GetUnreadNotifications(int recipientId);
        void MarkNotificationRead(int notificationId, int recipientId);
        Notification CreateNewNotification(Notification notification);
        List<Access.DataModels.Notification> GetMoreNotifications(int recipientId,int startIndex,int maxAmount);
        List<Access.DataModels.Notification> GetNotifications(int recipientId, int maxAmount);
    }
}

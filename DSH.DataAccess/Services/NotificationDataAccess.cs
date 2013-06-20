using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using DSH.Access;
using DSH.Access.PostAccess.Model;

namespace DSH.DataAccess.Services
{
    public class NotificationDataAccess : INotificationDataAccess
    {
        private readonly DoitShareitDataContext _dataContext;

        public NotificationDataAccess()
        {
            _dataContext = new DoitShareitDataContext();
        }

        public List<Access.DataModels.Notification> GetUnreadNotifications(int recipientId)
        {
            var notifications = from n in _dataContext.Notifications
                                where n.RecipientId == recipientId &&  n.IsRead == false
                                select n;
            var result = new List<Access.DataModels.Notification>();
            try
            {
                var querry = notifications.ToList();
                var userDataAccess = new UserDataAccess();
                for (int i = 0; i < querry.Count(); i++)
                {
                    var n = Mapper.Map<Notification, Access.DataModels.Notification>(querry[i]);
                    var sender = userDataAccess.GetUser((int)n.SenderId);
                    n.SenderPicUrl = sender.PicLocation;
                    n.SenderDisplayName = sender.DisplayName;
                    result.Add(n);
                }
            }
            catch (Exception)
            {

                return result;
            }

            return result;
        }

        public void MarkNotificationRead(int notificationId, int recipientId)
        {
            var notification = _dataContext.Notifications.Single(n => n.Id == notificationId && n.RecipientId==recipientId);
            if (notification != null) notification.IsRead = true;
            _dataContext.SubmitChanges();
        }

        public Access.DataModels.Notification CreateNewNotification(Access.DataModels.Notification notification)
        {
            notification.Id = null;
            var dbNotification = Mapper.Map<Access.DataModels.Notification, Notification>(notification);
            _dataContext.Notifications.InsertOnSubmit(dbNotification);
            _dataContext.SubmitChanges();
            return Mapper.Map<Notification, Access.DataModels.Notification>(dbNotification);
        }

        public List<Access.DataModels.Notification> GetMoreNotifications(int recipientId,int startIndex,int maxAmount)
        {
            var notifications = from n in _dataContext.Notifications
                                where n.RecipientId == recipientId
                                orderby n.DateOfOrigin descending
                                select n;

            var result = new List<Access.DataModels.Notification>();
            var querry = notifications.ToArray().Skip(startIndex).ToArray();
            var userDataAccess = new UserDataAccess();
            for (int i = 0; i < Math.Min(querry.Count(), maxAmount); i++)
            {
                var n = Mapper.Map<Notification, Access.DataModels.Notification>(querry[i]);
                var sender = userDataAccess.GetUser((int)n.SenderId);
                n.SenderPicUrl = sender.PicLocation;
                n.SenderDisplayName = sender.DisplayName;
                result.Add(n);
            }

            return result;
        }

        public List<Access.DataModels.Notification> GetNotifications(int recipientId, int maxAmount)
        {
            var notifications = from n in _dataContext.Notifications
                                where n.RecipientId == recipientId
                                orderby n.DateOfOrigin descending 
                                select n;

            var result = new List<Access.DataModels.Notification>();
            var querry = notifications.ToList();
            var userDataAccess = new UserDataAccess();
            for (int i = 0; i < Math.Min(querry.Count(),maxAmount); i++)
            {
                var n = Mapper.Map<Notification, Access.DataModels.Notification>(querry[i]);
                var sender = userDataAccess.GetUser((int)n.SenderId);
                n.SenderPicUrl = sender.PicLocation;
                n.SenderDisplayName = sender.DisplayName;
                result.Add(n);
            }

            return result;
        }
    }
}

using System;
using AutoMapper;
using DSH.Access.DataModels;
using DSH.Access.PostAccess.Model;
using DSH.DataAccess.Services;
using NUnit.Framework;

namespace DSH.DataAccess.Test
{
    [TestFixture]
    public class NotificationAccessTest
    {
        private INotificationDataAccess _notificationDataAccess;
        [SetUp]
        public void Init()
        {
            _notificationDataAccess = new NotificationDataAccess();
            Mapper.CreateMap<DSH.Access.DataModels.Notification, DSH.DataAccess.Notification>();
            Mapper.CreateMap<DSH.DataAccess.Notification, DSH.Access.DataModels.Notification>();


            Mapper.CreateMap<Users, DSH.DataAccess.User>();
            Mapper.CreateMap<DSH.DataAccess.User, Users>();
        }

        [Test]
        public void TestCreateNewNotification()
        {
            var notification = new Access.DataModels.Notification {SenderId = 17, RecipientId = 16, IsRead = false,Body="Thilini posted on your wall", RelevantPostId = 252, DateOfOrigin = DateTime.Now};
            _notificationDataAccess.CreateNewNotification(notification);
        }

        [Test]
        public void TestGetNotifications()
        {
            var notification = _notificationDataAccess.GetUnreadNotifications(16);
            Assert.NotNull(notification);
        }

        [Test]
        public void TestMarkNotificationRead()
        {
            _notificationDataAccess.MarkNotificationRead(0,16);
        }
    }
}

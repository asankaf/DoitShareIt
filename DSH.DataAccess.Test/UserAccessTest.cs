using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using DSH.Access.UserAccess;
using DSH.Access.UserAccess.Model;
using DSH.DataAccess.Services;
using NUnit.Framework;

namespace DSH.DataAccess.Test
{
    [TestFixture]
    public class UserAccessTest
    {
        private IUserDataAccess _userDataAccess;
        [SetUp]
        public void Init()
        {
            Configure();
            _userDataAccess = new UserDataAccess();
        }

        [Test]
        public void TestInsertUser()
        {
            string userUniqueId = Guid.NewGuid().ToString().Substring(0, 18);
            var user = new Users
                           {
                               DisplayName = "John Fowler",
                               CreationDate = DateTime.Now,
                               Reputation = 100,
                               Downvotes = 12,
                               Upvotes = 11,
                               LastAccessDate = DateTime.Now,
                               PicLocation = String.Empty,
                               PublicProfileUrl = String.Empty,
                               UserUniqueid = userUniqueId,
                               Views = 120
                           };
            _userDataAccess.InsertUserInfo(user);
            Assert.IsNotNull(_userDataAccess.GetUserInfo(userUniqueId));
        }

        private void Configure()
        {
            Mapper.CreateMap<Users, DSH.DataAccess.User>();
            Mapper.CreateMap<DSH.DataAccess.User, Users>();
        }
    }


}

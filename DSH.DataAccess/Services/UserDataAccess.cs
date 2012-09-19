using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using DSH.Access;
using DSH.Access.DataModels;
using DSH.Access.UserAccess;
using DSH.Access.UserAccess.Model;

namespace DSH.DataAccess.Services
{
    public class UserDataAccess : IUserDataAccess
    {
        
        private DoitShareitDataContext _dataContext;

        public UserDataAccess()
        {
            _dataContext = new DoitShareitDataContext();
        }

        public Users GetUserInfo(string userUniqueId)
        {           
            var userdetail = from user in _dataContext.Users
                               where user.UserUniqueid == userUniqueId
                               select user;
            if (userdetail.Count() > 1)
                throw new UniqueUserViolationXception("there exists more than one user with same uniqueId");
            else if (!userdetail.Any()) return null;
            else
            {
                return Mapper.Map<DSH.DataAccess.User, Users>(userdetail.ToList()[0]);
            }
        }

        public void InsertUserInfo(Users userInfo)
        {
            DSH.DataAccess.User user = Mapper.Map<Users, DSH.DataAccess.User>(userInfo);
            _dataContext.Users.InsertOnSubmit(user);
            _dataContext.SubmitChanges();
        }
    }
}

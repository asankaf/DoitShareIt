using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using AutoMapper;
using DSH.Access;
using DSH.Access.DataModels;
using DSH.Access.UserAccess;
using DSH.Main.Web.Services;

namespace DSH.DataAccess.Services
{
    public class UserDataAccess : IUserDataAccess
    {
        private readonly DoitShareitDataContext _dataContext;

        public UserDataAccess()
        {
            _dataContext = new DoitShareitDataContext();
        }

        public Users GetUser(int userId)
        {
            var user = from u in _dataContext.Users
                             where u.Id == userId
                             select u;

            if (user.Count() > 1)
                throw new UniqueUserViolationXception("there exists more than one user with same uniqueId");
            else if (!user.Any()) return null;
            else
            {
                var u = Mapper.Map<DSH.DataAccess.User, Users>(user.ToList()[0]);
                return u;
            }
        }

        public Users GetUserInfo(string userUniqueId)
        {
            IQueryable<User> userdetail = from user in _dataContext.Users
                                          where user.UserUniqueid == userUniqueId
                                          select user;

            if (userdetail.Count() > 1)
                throw new UniqueUserViolationXception("there exists more than one user with same uniqueId");
            else if (!userdetail.Any()) return null;
            else
            {
                return Mapper.Map<User, Users>(userdetail.ToList()[0]);
            }
        }

        public string GetUserPicUrl(int userId)
        {
            var user = from u in _dataContext.Users
                       where u.Id == userId
                       select u;

            if (user.Count() > 1)
                throw new UniqueUserViolationXception("there exists more than one user with same uniqueId");
            else if (!user.Any()) return null;
            else
            {
                return user.ToList()[0].PicLocation;
            }
        }

        public void InsertUserInfo(Users userInfo)
        {
            // this is the time this user is created and the first/last access
            userInfo.CreationDate = DateTime.Now;
            userInfo.LastAccessDate = userInfo.CreationDate;

            DiskDataAccess.SaveImageToDisk(DiskDataAccess.ServerPath, ref userInfo);


            User user = Mapper.Map<Users, User>(userInfo);
            _dataContext.Users.InsertOnSubmit(user);
            _dataContext.SubmitChanges();
        }

        /// <summary>
        /// Update the DataBase user information from the the given information
        /// </summary>
        /// <param name="user">New user info</param>
        /// <param name="userFromDb">DataModels.Users.PicLocation of the old information</param>
        public void UpdateUserInfo(Users user, Users userFromDb) // oldPicFName is old profile pic file name
        {
            // things to be updated
            // LastAccessDate
            // Profile picture
            // public profile URL
            // Display name of the user

            var oldPicFName = userFromDb.PicLocation;

            userFromDb.LastAccessDate = DateTime.Now;

            bool newFileSaved;

            string profilePictureFilePath;
            string fileName;

            DiskDataAccess.ProfilePicturePath(user, DiskDataAccess.ServerPath,
                DiskDataAccess.picturesFolder,
                out fileName, 
                out profilePictureFilePath);

            if (!File.Exists(profilePictureFilePath))
            {            
                DiskDataAccess.SaveImageToDisk(DiskDataAccess.ServerPath, ref user);
                userFromDb.PicLocation = user.PicLocation;
                if(oldPicFName != userFromDb.PicLocation) DiskDataAccess.TakeCareOfOldImage(DiskDataAccess.ServerPath, oldPicFName);  // this line is commented coz 
                // database doesn't get updated due to some issue, as the database update issue is fixed we can use this line of code
            }



            UpdateUser(userFromDb);

/*            var userFromDb = (from usr in _dataContext.Users
                              where usr.UserUniqueid == user.UserUniqueid
                              select user);

            // updating the user from database
            if (userFromDb.Any())
            {
                Users thisUser = userFromDb.First();

                thisUser.LastAccessDate = user.LastAccessDate;
                thisUser.PicLocation = user.PicLocation;
                thisUser.PublicProfileUrl = user.PublicProfileUrl;
                thisUser.DisplayName = user.DisplayName;
                
                _dataContext.SubmitChanges();

            }*/



            
        }

        public List<Users> MatchUser(string searchText, int maxResults)
        {
            // matching searched user
            var result = from n in _dataContext.Users
                         where n.DisplayName.Contains(searchText)
                         orderby n.DisplayName
                         select n;

            if (!result.Any()) return null;
            else
            {
                return Mapper.Map<List<DSH.DataAccess.User>, List<DSH.Access.DataModels.Users>>(result.Take(maxResults).ToList());
            }
        }

        public Users UpdateUser(Users user)
        {
            var id = user.Id;
            var dbUser = from u in _dataContext.Users
                         where u.UserUniqueid == user.UserUniqueid
                         select u;
            if(dbUser.Count()>1) throw new UniqueUserViolationXception("There exist more than one user with the given user unique id");
            else if (!dbUser.Any()) throw new Exception("no user exists with given unique user id for update");
            else
            {
                dbUser.ToList()[0].DisplayName = user.DisplayName;
                dbUser.ToList()[0].CreationDate = user.CreationDate;
                dbUser.ToList()[0].Reputation = user.Reputation;
                dbUser.ToList()[0].DownVotes = user.Downvotes;
                dbUser.ToList()[0].UpVotes = user.Upvotes;
                dbUser.ToList()[0].LastAccessDate = user.LastAccessDate;
                dbUser.ToList()[0].PicLocation = user.PicLocation;
                dbUser.ToList()[0].PublicProfileUrl = user.PublicProfileUrl;
                dbUser.ToList()[0].UserUniqueid = user.UserUniqueid;
                dbUser.ToList()[0].SessionKey = user.SessionKey;
                dbUser.ToList()[0].Views = user.Views;


                _dataContext.SubmitChanges();
                return Mapper.Map<User, Access.DataModels.Users>(dbUser.ToArray()[0]);
            }
        }
    }
}
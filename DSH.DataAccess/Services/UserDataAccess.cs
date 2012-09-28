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

        public void InsertUserInfo(Users userInfo)
        {
            // this is the time this user is created and the first/last access
            userInfo.CreationDate = DateTime.Now;
            userInfo.LastAccessDate = userInfo.CreationDate;

            using (var webClient = new WebClient())
            {
                // appDataFolder is the place where profile pictures from LinkedIn are stored
                // appDataFolder location = "web application dir" / image_store"
                string picturesFolder = "image_store";
                string appDataFolder = Path.Combine(HttpContext.Current.Server.MapPath(@"~/"), picturesFolder);


                string url = userInfo.PicLocation;

                Byte[] hashCode = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(url));
                var hashStringB = new StringBuilder();

                foreach (byte b in hashCode)
                {
                    hashStringB.Append(b.ToString());
                }

                string fileName = hashStringB.ToString();
                fileName += ".jpg";
                string profilePictureFilePath = Path.Combine(appDataFolder, fileName);

                try
                {
                    // all LinedIn profile picture files are jpg files
                    webClient.DownloadFile(userInfo.PicLocation, profilePictureFilePath);
                    userInfo.PicLocation = "../" + "image_store" + "/" + fileName;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }


            User user = Mapper.Map<Users, User>(userInfo);
            _dataContext.Users.InsertOnSubmit(user);
            _dataContext.SubmitChanges();
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
                dbUser.ToList()[0].Downvotes = user.Downvotes;
                dbUser.ToList()[0].Upvotes = user.Upvotes;
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
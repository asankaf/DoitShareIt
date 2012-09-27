using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using DSH.Access;
using DSH.Access.UserAccess;
using DSH.Access.DataModels;




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
            // this is the time this user is created and the first/last access
            userInfo.CreationDate = DateTime.Now;
            userInfo.LastAccessDate = userInfo.CreationDate;

            using (var webClient = new WebClient())
            {
                // appDataFolder is the place where profile pictures from LinkedIn are stored
                // appDataFolder location = "web application dir" / "App_Data" / "Pictures"
                string picturesFolder = "image_store";
                string appDataFolder = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/"), picturesFolder );

                Byte[] hashCode = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(userInfo.PicLocation));
                StringBuilder hashStringB = new StringBuilder();

                foreach (var b in hashCode)
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
                    userInfo.PicLocation = "../" + "image_store"  + "/" + fileName;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }
            

            DSH.DataAccess.User user = Mapper.Map<Users, DSH.DataAccess.User>(userInfo);
            _dataContext.Users.InsertOnSubmit(user);
            _dataContext.SubmitChanges();
        }


        public List<DSH.Access.DataModels.Post> GetUserPost(string userUniqeId)
        {

            // Return Post of the paritculer user whos UniqeId is provided

            var user = this.GetUserInfo(userUniqeId);

            var userPost = from posts in _dataContext.Posts
                           where posts.OwnerUserId == user.Id
                           select posts;

            return Mapper.Map<List<DSH.DataAccess.Post>, List<DSH.Access.DataModels.Post>>(userPost.ToList());
        }
    }
}

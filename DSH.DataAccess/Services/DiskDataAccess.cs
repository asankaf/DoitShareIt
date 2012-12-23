using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using DSH.Access.DataModels;

namespace DSH.Main.Web.Services
{
    public static class DiskDataAccess
    {
        /// <summary>
        /// Generate the file path where the profile picture of a new user will be saved in the disk
        /// </summary>
        /// <param name="userInfo">Users object of the new user</param>
        /// <param name="picturesFolder">Name of the picture folder</param>
        /// <param name="fileName">out: unique name of the file</param>
        /// <param name="profilePictureFilePath">out: path where to save new file</param>
        public static void ProfilePicturePath(Users userInfo, string serverPath, string picturesFolder, out string fileName,
                                       out string profilePictureFilePath)
        {
            string picDataFolder = Path.Combine(serverPath, picturesFolder);

            /*
             * if the linkedin user have not set a profile picture yet userInfo.PicLocation will be a null
             * then handle it with grace
             */

            if (userInfo.PicLocation != null)
            {
                string url = userInfo.PicLocation;

                Byte[] hashCode = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(url));
                var hashStringB = new StringBuilder();

                foreach (byte b in hashCode)
                {
                    hashStringB.Append(b.ToString());
                }

                fileName = hashStringB.ToString();
                fileName += ".jpg";
                profilePictureFilePath = Path.Combine(picDataFolder, fileName);
            }
            else
            {
                fileName = "unknown.jpg";
                profilePictureFilePath = Path.Combine(picDataFolder, fileName);
            }
        }



        /// <summary>
        /// Save the public picture in the disk and alter the Users object so our new location in reflected in
        /// its public picture URL field.
        /// </summary>
        /// <param name="serverPath">Path to the server root</param>
        /// <param name="userInfo">Users object of the new user to be insert into database</param>
        public static void SaveImageToDisk(string serverPath, ref Users userInfo)
        {
            using (var webClient = new WebClient())
            {
                // appDataFolder is the place where profile pictures from LinkedIn are stored
                // appDataFolder location = "web application dir" / image_store"
                const string picturesFolder = "image_store";
                string fileName = string.Empty;
                string profilePictureFilePath = string.Empty;

                DiskDataAccess.ProfilePicturePath(userInfo, serverPath, picturesFolder, out fileName, out profilePictureFilePath);

                try
                {
                    // all LinedIn profile picture files are jpg files
                    // userInfo.PicLocation is null means linked in user have not set a profile picture yet so 
                    // there is nothing to be downloaded so there is a pre downloaded file in image_store
                    // so we can use it
                    if (userInfo.PicLocation != null)
                    {
                         webClient.DownloadFile(userInfo.PicLocation, profilePictureFilePath);
                    }
                   
                    // altering the userInfo picture location to point to our new pic location
                    userInfo.PicLocation = "../" + picturesFolder + "/" + fileName;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static string ServerPath
        {
            get
            {
                string serverPath = HttpContext.Current.Server.MapPath(@"~/");
                return serverPath;
            }
        }
    }
}

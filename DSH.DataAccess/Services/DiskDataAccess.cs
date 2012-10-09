using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using DSH.Access.DataModels;

namespace DSH.DataAccess.Services
{
    static class DiskDataAccess
    {
        /// <summary>
        /// Generate the file path where the profile picture of a new user will be saved in the disk
        /// </summary>
        /// <param name="userInfo">Users object of the new user</param>
        /// <param name="picturesFolder">Name of the picture folder</param>
        /// <param name="fileName">out: unique name of the file</param>
        /// <param name="profilePictureFilePath">out: path where to save new file</param>
        internal static void ProfilePicturePath(Users userInfo, string picturesFolder, out string fileName,
                                       out string profilePictureFilePath)
        {
            string picDataFolder = Path.Combine(HttpContext.Current.Server.MapPath(@"~/"), picturesFolder);


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

    }
}

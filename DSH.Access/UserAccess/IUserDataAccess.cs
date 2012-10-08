using DSH.Access.DataModels;
using System.Collections.Generic;

namespace DSH.Access.UserAccess
{
    public interface IUserDataAccess
    {
        Users GetUserInfo(string userUniqueId);
        void InsertUserInfo(Users userInfo);
        Users GetUser(int userId);

        /****** todo: implement 
         * 
        Post[] GetUserPosts(string userUniqueId);
        Post[] GetPublicWallPosts(string userUniquId);
         * 
         * 
         */
    }
}

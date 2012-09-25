using DSH.Access.DataModels;
using System.Collections.Generic;

namespace DSH.Access.UserAccess
{
    public interface IUserDataAccess
    {
        Users GetUserInfo(string userUniqueId);
        void InsertUserInfo(Users userInfo);

        List<Post> GetUserPost(string userUniqeId);


        /****** todo: implement 
         * 
        Post[] GetUserPosts(string userUniqueId);
        Post[] GetPublicWallPosts(string userUniquId);
         * 
         * 
         */
    }
}

using DSH.Access.UserAccess.Model;

namespace DSH.Access.UserAccess
{
    public interface IUserDataAccess
    {
        Users GetUserInfo(string userUniqueId);
        void InsertUserInfo(Users userInfo);
    }
}

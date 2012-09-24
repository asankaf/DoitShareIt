using DSH.Access.DataModels;

namespace DSH.Access.UserAccess
{
    public interface IUserDataAccess
    {
        Users GetUserInfo(string userUniqueId);
        void InsertUserInfo(Users userInfo);
    }
}

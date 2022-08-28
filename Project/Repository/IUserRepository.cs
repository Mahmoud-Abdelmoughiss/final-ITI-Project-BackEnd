using EcommerseApplication.Models;

namespace EcommerseApplication.Repository
{
    public interface IUserRepository
    {
        int AddUser(User NewUser);
        int DeleteUser(int Id);
        List<User> GetAllUsers();
        User GetUserById(int Id);
        User GetUserByIdentityId(string IdentityId);
        int UpdateUser(int Id, User NewUser);
        bool SetStripeTokenID(int UserID, string StripeToken);
    }
}
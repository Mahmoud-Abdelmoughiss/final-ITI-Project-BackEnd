using EcommerseApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerseApplication.Repository
{
    public class UserRepository : IUserRepository
    {
        Context context;
        public UserRepository(Context _context)
        {
            context = _context;
        }
        public List<User> GetAllUsers()
        {
            List<User> users = context.users.ToList();
            return users;
        }
        public User GetUserById(int Id)
        {
            User user = context.users.Include(u2 => u2.Identity).Include(us => us.User_Pyment).FirstOrDefault(u => u.Id == Id);
            return user;
        }
        public User GetUserByIdentityId(string IdentityId)
        {
            User user = context.users.Include(us=>us.User_Pyment).Include(u2 => u2.Identity).FirstOrDefault(u => u.IdentityId == IdentityId);
            return user;
        }
        public int AddUser(User NewUser)
        {
            if (NewUser != null)
            {
                context.users.Add(NewUser);
                return context.SaveChanges();
            }
            return 0;
        }
        public int UpdateUser(int Id, User NewUser)
        {
            User olduser = context.users.FirstOrDefault(u => u.Id == Id);
            if (olduser != null)
            {
               
                    olduser.FirstName = NewUser.FirstName;
                    olduser.LastName = NewUser.LastName;
                    olduser.FirstNameAR = NewUser.FirstNameAR;
                    olduser.LastNameAR = NewUser.LastNameAR;
                    olduser.UserName = NewUser.UserName;
                    olduser.UserNameAR = NewUser.UserNameAR;
                    olduser.Phone = NewUser.Phone;
                    olduser.Phone = NewUser.Phone;
                    olduser.birthDate = NewUser.birthDate;
                    return context.SaveChanges();   
            }
            return 0;
        }
        public int DeleteUser(int Id)
        {
            User user = context.users.FirstOrDefault(u => u.Id == Id);
            if (user != null)
            {
                context.users.Remove(user);
                return context.SaveChanges();
            }
            return 0;
        }
        public bool SetStripeTokenID(int UserID, string StripeToken)
        {
            User user = context.users.FirstOrDefault(u => u.Id == UserID);
            if (user != null)
            {
                user.StripeTokenID = StripeToken;
                context.SaveChanges();
                return true;
            }
            else { return false; }
        }
    }
}

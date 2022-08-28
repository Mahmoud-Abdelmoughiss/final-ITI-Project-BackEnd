using EcommerseApplication.Models;
using EcommerseApplication.ViewModels;

namespace EcommerseApplication.Repository
{
    public class UserAddressRepository : IUserAddress
    {
        private readonly Context context;

        public UserAddressRepository(Context _con)
        {
            context = _con;
        }

        public User_address  GetAddress(int id)
        {
            return context.User_Addresses.FirstOrDefault(i => i.Id == id);
        }
        public void AddNewAddresss(int id,UserAddressUSerIdDTO NewAddress)
        {
            User_address user_Address = new User_address();
            user_Address.AddressLine1 = NewAddress.AddressLine1;
            user_Address.AddressLine2 = NewAddress.AddressLine2;
            user_Address.City = NewAddress.City;
            user_Address.Country = NewAddress.Country;
            user_Address.PostalCode = NewAddress.PostalCode;
            user_Address.mobile = NewAddress.mobile;
            user_Address.telephone = NewAddress.telephone;
            user_Address.UserId = id;
            context.User_Addresses.Add(user_Address);
            context.SaveChanges();
        }
        public void AddNewAddress(User_address user_Address)
        {
            context.User_Addresses.Add(user_Address);
            context.SaveChanges();
        }

        public void DeleteAddress(int id)
        {
           context.User_Addresses.Remove(GetAddress(id));
            context.SaveChanges();
        }

        public void UpdateAddress(int id, User_address user_Address)
        {
            User_address user_Addressold=GetAddress(id);
            user_Addressold.AddressLine1=user_Address.AddressLine1;
            user_Addressold.AddressLine2=user_Address.AddressLine2;
            user_Addressold.City=user_Address.City;
            user_Addressold.PostalCode=user_Address.PostalCode;
            user_Addressold.Country=user_Address.Country;
            user_Addressold.telephone = user_Address.telephone;
            user_Addressold.mobile=user_Address.mobile;
            context.SaveChanges();
        }

        public List<User_address> GetAllAddress(int id)
        {
            return context.User_Addresses.Where(a => a.UserId==id).ToList();
        }
    }
}

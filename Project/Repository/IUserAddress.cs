using EcommerseApplication.Models;
using EcommerseApplication.ViewModels;

namespace EcommerseApplication.Repository
{
    public interface IUserAddress
    {
        public void AddNewAddress(User_address user_Address);
        public void AddNewAddresss(int id,UserAddressUSerIdDTO NewAddress);
        public void UpdateAddress(int id,User_address user_Address);
        public void DeleteAddress(int id);
        public User_address GetAddress(int id);
        public List<User_address> GetAllAddress(int id);

    }
}

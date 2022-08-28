using EcommerseApplication.DTO;
using EcommerseApplication.Models;

namespace EcommerseApplication.Repository
{
    public interface IUserPayement
    {
        public void AddUSerPayment(User_Payement newPaement);
        public void AddUSerPaymentt(UserPaymentDTO newpayement);
        public void DeleteUserPayment(int id);
        public User_Payement GetUserPayment(int id);
        public List<User_Payement> GetAllByUser(int UserID);
        public void updateUserPayement(int id,User_Payement newPaement);

        bool SetPaymentTokenID(int PaymentID, String PaymentToken);

    }
}

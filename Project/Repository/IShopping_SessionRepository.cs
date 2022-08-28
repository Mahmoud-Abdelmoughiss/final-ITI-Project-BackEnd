using EcommerseApplication.Models;

namespace EcommerseApplication.Repository
{
    public interface IShopping_SessionRepository
    {
        int AddShopping_Session(Shopping_Session NewShopping_Session);
        int DeleteShopping_SessionById(int Id);
        List<Shopping_Session> GetAllShopping_Session();
        Shopping_Session GetShopping_SessionById(int Id);

        List<Shopping_Session> GetByUserId(int Id);
        void ClearTotal(int SessionId);
    }
}
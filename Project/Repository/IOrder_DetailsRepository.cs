using EcommerseApplication.Models;

namespace EcommerseApplication.Repository
{
    public interface IOrder_DetailsRepository
    {
        List<Order_Details> GetAll();
        List<Order_Details> GetAllByUserID(int UserID);
        Order_Details Get(int Id);
        void Create(Order_Details Order_Details);
        void Update(int Id, Order_Details Order_Details);
        void Delete(int Id);
    }
}

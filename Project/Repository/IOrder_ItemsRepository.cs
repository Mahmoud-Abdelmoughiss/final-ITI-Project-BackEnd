using EcommerseApplication.Models;

namespace EcommerseApplication.Repository
{
    public interface IOrder_ItemsRepository
    {
        List<Order_Items> GetAll();
        Order_Items Get(int Id);
        void Create(Order_Items Order_Items);
        void Update(int Id, Order_Items Order_Items);
        void Delete(int Id);
    }
}

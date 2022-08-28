using EcommerseApplication.Models;

namespace EcommerseApplication.Repository
{
    public class Order_ItemsRepository : IOrder_ItemsRepository
    {
        private readonly Context context;

        public Order_ItemsRepository(Context _context)
        {
            context = _context;
        }

        public List<Order_Items> GetAll()
        {
            return context.order_items.Where(o => o.DeletedAt == null).ToList();
        }
        public Order_Items Get(int Id)
        {
            return context.order_items.Where(o => o.DeletedAt == null).FirstOrDefault(o => o.ID == Id);
        }

        public void Create(Order_Items Order_Items)
        {
            Order_Items.CreatedAt = DateTime.Now;
            context.order_items.Add(Order_Items);
            context.SaveChanges();
        }

        public void Delete(int Id)
        {
            context.order_items.Remove(Get(Id));
            context.SaveChanges();
        }

        public void Update(int Id, Order_Items Order_Items)
        {
            Order_Items OldOrder_Items = Get(Id);
            OldOrder_Items.Quantity = Order_Items.Quantity;
            OldOrder_Items.OrderID = Order_Items.OrderID;
            OldOrder_Items.ProductID = Order_Items.ProductID;

            OldOrder_Items.UpdatedAt = DateTime.Now;

            context.SaveChanges();
        }
    }
}

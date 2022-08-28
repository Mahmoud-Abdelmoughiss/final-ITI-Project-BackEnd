using EcommerseApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerseApplication.Repository
{
    public class Order_DetailsRepository : IOrder_DetailsRepository
    {
        private readonly Context context;

        public Order_DetailsRepository(Context _context)
        {
            context = _context;
        }
        public List<Order_Details> GetAll()
        {
            return context.order_Details.Where(o => o.DeletedAt == null).ToList();
        }
        public List<Order_Details> GetAllByUserID(int UserID)
        {
            return context.order_Details.Include(d=>d.Order_Items).ThenInclude(i=>i.Product).ThenInclude(p => p.Product_Images).Where(o => o.UserID == UserID).ToList();
        }
        public Order_Details Get(int Id)
        {
            return context.order_Details.Include(d=>d.Order_Items).Where(o => o.DeletedAt == null).FirstOrDefault(or=>or.Id == Id);
        }

        public void Create(Order_Details Order_Details)
        {
            Order_Details.CreatedAt = DateTime.Now;
            context.order_Details.Add(Order_Details);
            context.SaveChanges();
        }

        public void Delete(int Id)
        {
            context.order_Details.Remove(Get(Id));
            context.SaveChanges();
        }

        public void Update(int Id, Order_Details Order_Details)
        {
            Order_Details OldOrder_Details = Get(Id);
            OldOrder_Details.Total = Order_Details.Total;
            OldOrder_Details.Payment_ID = Order_Details.Payment_ID;
            OldOrder_Details.UserID = Order_Details.UserID;

            OldOrder_Details.UpdatedAt = DateTime.Now;

            context.SaveChanges();
        }
    }
}

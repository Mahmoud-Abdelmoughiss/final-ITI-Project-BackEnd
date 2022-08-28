using EcommerseApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerseApplication.Repository
{
    public class Cart_ItemRepository : ICart_ItemRepository
    {
        Context context;
        public Cart_ItemRepository(Context _context)
        {
            context = _context;
        }
        public List<Cart_Item> GetAllCart_Items()
        {
            List<Cart_Item> cart_Item = context.Cart_Items.ToList();
            return cart_Item;
        }

        public List<Cart_Item> GetAllCart_ItemsBySession(int id)
        {
            List<Cart_Item> cart_Item = context.Cart_Items.Where(e=>e.SessionId == id).ToList();
            return cart_Item;
        }
        public Cart_Item GetCart_ItemById(int Id)
        {
            Cart_Item cart_Item = context.Cart_Items.FirstOrDefault(x => x.Id == Id);
            return cart_Item;
        }
        public Cart_Item GetCardItemByproductAndSession(int sessionID,int productId)
        {
          return  context.Cart_Items.Where(e => e.SessionId == sessionID).FirstOrDefault(pd => pd.ProductId == productId);
        }
        public int AddCart_Item(Cart_Item NewCart_Item)
        {
            if (NewCart_Item != null)
            {
                context.Cart_Items.Add(NewCart_Item);
                context.SaveChanges();
            }
            return 0;

        }
        public int DeleteCart_ItemById(int Id)
        {
            Cart_Item cart_Item = context.Cart_Items.FirstOrDefault(x => x.Id == Id);
            if (cart_Item != null)
            {
                try
                {
                    context.Cart_Items.Remove(cart_Item);
                    return context.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
            return 0;
        }
        public int DeleteCart_Item(Cart_Item cart_Item)
        {
            
            if (cart_Item != null)
            {
                try
                {
                    context.Cart_Items.Remove(cart_Item);
                    return context.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
            return 0;
        }
        public int UpdateCart_Item(int Id, Cart_Item NewCart_Item)
        {
            Cart_Item oldCart_Item = context.Cart_Items.FirstOrDefault(ci => ci.Id == Id);
            if (oldCart_Item != null)
            {
                oldCart_Item.SessionId = NewCart_Item.SessionId;
                oldCart_Item.Quantity = NewCart_Item.Quantity;
                oldCart_Item.ProductId = NewCart_Item.ProductId;
                oldCart_Item.CreatedAt = NewCart_Item.CreatedAt;
                oldCart_Item.UpdatedAt = NewCart_Item.UpdatedAt;
                oldCart_Item.DeletedAt = NewCart_Item.DeletedAt;
                return context.SaveChanges();
            }
            return 0;
        }


        public List<Cart_Item> GetAllBySessionID(int ShoppingSessionID)
        {
            return context.Cart_Items.Include(c=>c.product).ThenInclude(p=>p.Product_Inventory).Include(c => c.product.Product_Images).Where(c => c.SessionId == ShoppingSessionID).ToList();
        }
    }
}

using EcommerseApplication.Models;

namespace EcommerseApplication.Repository
{
    public interface ICart_ItemRepository
    {
        int AddCart_Item(Cart_Item NewCart_Item);
        int DeleteCart_ItemById(int Id);
        List<Cart_Item> GetAllCart_Items();
        Cart_Item GetCart_ItemById(int Id);
        int UpdateCart_Item(int Id, Cart_Item NewCart_Item);
        public Cart_Item GetCardItemByproductAndSession(int sessionID, int productId);

        int DeleteCart_Item(Cart_Item cart_Item);
        List<Cart_Item> GetAllBySessionID(int ShoppingSessionID);
        public List<Cart_Item> GetAllCart_ItemsBySession(int id);
    }
}
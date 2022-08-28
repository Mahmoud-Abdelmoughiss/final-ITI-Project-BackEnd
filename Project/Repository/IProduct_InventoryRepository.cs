using EcommerseApplication.Models;

namespace EcommerseApplication.Repository
{
    public interface IProduct_InventoryRepository
    {
        List<Product_Inventory> GetAll();
        Product_Inventory Get(int Id);
        void Create(Product_Inventory Product_Inventory);
        void Update(int Id, Product_Inventory Product_Inventory);
        void Delete(int Id);
        public int AddproductInventory(int Quentity);

        public void updateproductInventory(int Id, int NewQuentity);
    }
}

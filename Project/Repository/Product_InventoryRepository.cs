using EcommerseApplication.Models;

namespace EcommerseApplication.Repository
{
    public class Product_InventoryRepository : IProduct_InventoryRepository
    {
        private readonly Context context;

        public Product_InventoryRepository(Context _context)
        {
            context = _context;
        }

        public List<Product_Inventory> GetAll()
        {
            return context.Product_Inventorys.Where(p => p.DeletedAt == null).ToList();
        }
        public Product_Inventory Get(int Id)
        {
            return context.Product_Inventorys.Where(p => p.DeletedAt == null).FirstOrDefault(p => p.ID == Id);
        }
        
        public void Create(Product_Inventory Product_Inventory)
        {
            Product_Inventory.CreatedAt = DateTime.Now;
            context.Product_Inventorys.Add(Product_Inventory);
            context.SaveChanges();
        }
        public int AddproductInventory(int Quentity)
        {

            Product_Inventory Pinventory = new Product_Inventory();
            if (Quentity > 0)
            {
                try
                {
                    Pinventory.CreatedAt = DateTime.Now;
                    Pinventory.Quantity = Quentity;
                    context.Product_Inventorys.Add(Pinventory);
                    context.SaveChanges();
                    return Pinventory.ID;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            return 0;
        }

        public void Delete(int Id)
        {
            context.Product_Inventorys.Remove(Get(Id));
            context.SaveChanges();
        }

        public void Update(int Id, Product_Inventory Product_Inventory)
        {
            Product_Inventory OldProduct_Inventory = Get(Id);
            OldProduct_Inventory.Quantity = Product_Inventory.Quantity;

            OldProduct_Inventory.UpdatedAt = DateTime.Now;
        }
        public void updateproductInventory(int Id, int NewQuentity)
        {
            Product_Inventory productInventory = context.Product_Inventorys.FirstOrDefault(In => In.ID == Id);
            productInventory.Quantity = NewQuentity;
            productInventory.UpdatedAt = DateTime.Now;
            context.SaveChanges();
        }

       
    }
}

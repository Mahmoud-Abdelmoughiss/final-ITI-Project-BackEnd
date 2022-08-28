using EcommerseApplication.Models;

namespace EcommerseApplication.Repository
{
    public class Product_ImageRepository : IProduct_ImageRepository
    {
        private readonly Context context;

        public Product_ImageRepository(Context _context)
        {
            context = _context;
        }
        public List<Product_Images> GetAll()
        {
            return context.product_Images.Where(p => p.DeletedAt == null).ToList();
        }
        public Product_Images Get(int Id)
        {
            return context.product_Images.Where(p => p.DeletedAt == null).FirstOrDefault(p => p.Id == Id);
        }

        public void Create(Product_Images Product_Images)
        {
            Product_Images.CreatedAt = DateTime.Now;
            context.product_Images.Add(Product_Images);
            context.SaveChanges();
        }

        public void Delete(int Id)
        {
            context.product_Images.Remove(Get(Id));
            context.SaveChanges();
        }

        public void Update(int Id, Product_Images NewProduct_Images)
        {
            Product_Images OldProduct_Images = Get(Id);
            OldProduct_Images.UpdatedAt = DateTime.Now;
            OldProduct_Images.ImageFileName = NewProduct_Images.ImageFileName;
            OldProduct_Images.ProductID = NewProduct_Images.ProductID;

            context.SaveChanges();
        }
    }
}

using EcommerseApplication.Models;

namespace EcommerseApplication.Repository
{
    public interface IProduct_ImageRepository
    {
        List<Product_Images> GetAll();
        Product_Images Get(int Id);
        void Create(Product_Images Product_Images);
        void Update(int Id, Product_Images Product_Images);
        void Delete(int Id);
    }
}

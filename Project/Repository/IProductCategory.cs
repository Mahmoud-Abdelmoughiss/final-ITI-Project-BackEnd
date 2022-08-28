using EcommerseApplication.Models;

namespace EcommerseApplication.Respository
{
    public interface IProductCategory
    {
        public List<Product_Category> GetAllCategories();
        public List<Product_Category> GetAllWithSubCategory();
        public Product_Category GetCategoryByName(string name);
        public Product_Category GetCategoryByID(int id);
        public void AddCategory(Product_Category newCat);
        public void DeleteCategory(int id);
        public void UpdateCategory(int id, Product_Category _Category);
        public void UpdateOldCategory(Product_Category _Category);
        public int DeleteACategory(int Id);

    }
}

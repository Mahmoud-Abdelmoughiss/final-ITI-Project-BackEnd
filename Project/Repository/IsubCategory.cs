using EcommerseApplication.Models;
namespace EcommerseApplication.Repository
{
    public interface IsubCategory
    {
        public void insert(subCategory subCategory);
        public void update(int id, subCategory  subCategory);
        public subCategory getByID(int id);
        public subCategory getByName(string name);
        public List<subCategory> getAll();
        public List<subCategory> getAllByCategoryID(int categoryID);
        public void delete(int id);
        public List<subCategory> GetAllWithIncludeByCategoryID(int categoryID);
        public int DeletesubCategory(int Id);
        public void updateSubCategory(subCategory scategory);
    }
}

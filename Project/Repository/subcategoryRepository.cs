using EcommerseApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerseApplication.Repository
{
    public class subcategoryRepository : IsubCategory
    {
        Context context;
        public subcategoryRepository(Context _context)
        {
            this.context = _context;
        }
        public void delete(int id)
        {
           subCategory subCat= context.subCategories.FirstOrDefault(sub => sub.Id == id);
            context.subCategories.Remove(subCat);
            context.SaveChanges();
        }

        public List<subCategory> getAll()
        {
            return context.subCategories.Include(s=>s.category).ToList();
        }

        public List<subCategory> getAllByCategoryID(int categoryID)
        {
            return context.subCategories.Where(sub=>sub.CategoryId == categoryID).ToList();
        }

        public subCategory getByID(int id)
        {
            return context.subCategories.Include(s=>s.category).FirstOrDefault(sub => sub.Id == id);
        }

        public subCategory getByName(string name)
        {

            return context.subCategories.FirstOrDefault(sub => sub.Name == name);
        }

        public void insert(subCategory subCategory)
        {
            context.subCategories.Add(subCategory);
            context.SaveChanges();
        }

        public void update(int id, subCategory subCategory)
        {
            subCategory old= context.subCategories.FirstOrDefault(sub => sub.Id == id);
            old.Name=subCategory.Name;
            old.image = subCategory.image;
            old.arabicName=subCategory.arabicName;
            old.arabicDescription=subCategory.arabicDescription;
            old.CategoryId=subCategory.CategoryId;
           old.Description=subCategory.Description;
            context.SaveChanges();
        }

        public List<subCategory> GetAllWithIncludeByCategoryID(int CategoryID)
        {
            return context.subCategories.Include(s => s.category).Where(s=>s.CategoryId == CategoryID).ToList();
        }
        public int DeletesubCategory(int Id)
        {
            subCategory productsubCategory = getByID(Id);
            if (productsubCategory != null)
            {
                context.subCategories.Remove(productsubCategory);
                return context.SaveChanges();
            }
            return 0;

        }
        public void updateSubCategory(subCategory scategory)
        {
            context.SaveChanges();
        }
    }
}

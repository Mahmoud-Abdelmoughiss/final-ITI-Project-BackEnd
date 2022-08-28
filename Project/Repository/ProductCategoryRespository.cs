using EcommerseApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerseApplication.Respository
{
    public class ProductCategoryRespository : IProductCategory
    {
        private readonly Context context;

        public ProductCategoryRespository(Context _context)
        {
            context = _context;
        }

        public void AddCategory(Product_Category newCat)
        {
            
                string name = newCat.Name;
                Product_Category olds = context.Product_Categorys.FirstOrDefault(i=>i.Name==name);
                if (olds == null)
                {
                    context.Product_Categorys.Add(newCat);
                    context.SaveChanges();
                }
                 
        }
        

        public void DeleteCategory(int id)
        {
            Product_Category category = context.Product_Categorys.FirstOrDefault(i => i.ID == id);
            context.Remove(category);
            context.SaveChanges();
        }

        public List<Product_Category> GetAllCategories()
        {
           List<Product_Category> Pr=new List<Product_Category>();
            Pr = context.Product_Categorys.ToList();
            return Pr;
        }

        public Product_Category GetCategoryByID(int id)
        {
            return context.Product_Categorys.FirstOrDefault(i => i.ID == id);
        }

        public Product_Category GetCategoryByName(string name)
        {
           return context.Product_Categorys.FirstOrDefault(i => i.Name == name);
        }
        public int DeleteACategory(int Id)
        {
            Product_Category productCategory = GetCategoryByID(Id);
            if (productCategory != null)
            {
                context.Product_Categorys.Remove(productCategory);
                return context.SaveChanges();
            }
            return 0;

        }
        public void UpdateCategory(int id,Product_Category _Category)
        {
            Product_Category old = context.Product_Categorys.FirstOrDefault(i => i.ID == id);
            old.Name = _Category.Name;
            old.UpdatedAt = DateTime.Now;
            old.Description=_Category.Description;
            context.SaveChanges();
        }
        public void UpdateOldCategory( Product_Category _Category)
        {
            context.SaveChanges();
        }



        public List<Product_Category> GetAllWithSubCategory()
        {
            return context.Product_Categorys.Include(p => p.SubCategories).ToList();
        }
    }
}

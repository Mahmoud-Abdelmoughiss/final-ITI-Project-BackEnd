using EcommerseApplication.Data;
using EcommerseApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerseApplication.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context context;

        public ProductRepository(Context _context)
        {
            context = _context;
        }
        public List<Product> GetAll()
        {
            return context.Products.Where(p => p.DeletedAt == null)
                                   .Where(p2 => p2.StatusApproval == ProductApprovelEnum.Approved.ToString()).ToList();
        }
        public List<Product> GetAllApprovedd(int partnerId)
        {
            return context.Products.Include(p => p.Discount)
                .Include(p => p.Partener)
                .Include(p => p.Product_Images)
                .Include(p => p.subcategory)
                .Include(p => p.Product_Category)
                .Include(p => p.Product_Inventory).Where(pp => pp.PartenerID == partnerId).Where(ppp => ppp.DiscountID == null)
                .Where(p2 => p2.StatusApproval == ProductApprovelEnum.Approved.ToString()).ToList();
        }
        public List<Product> GetAllNotApproved()
        {
            return context.Products.Include(p => p.Discount)
                .Include(p => p.Partener)
                .Include(p => p.Product_Images)
                .Include(p => p.subcategory)
                .Include(p => p.Product_Category)
                .Include(p => p.Product_Inventory)
                .Where(p => p.DeletedAt == null)
                .Where(p2=>p2.StatusApproval != ProductApprovelEnum.Approved.ToString()).ToList();
        }
        public List<Product> GetAllApproved()
        {
            return context.Products.Include(p => p.Discount)
                .Include(p => p.Partener)
                .Include(p => p.Product_Images)
                .Include(p => p.subcategory)
                .Include(p => p.Product_Category)
                .Include(p => p.Product_Inventory)
                .Where(p => p.DeletedAt == null)
                .Where(p2 => p2.StatusApproval == ProductApprovelEnum.Approved.ToString()).ToList();
        }
        public void ReduseQuantity(int ProductID, int DecreasedQuantity)
        {
            Product pro = context.Products.Include(p => p.Product_Inventory).FirstOrDefault(p1 => p1.ID == ProductID);
            pro.Product_Inventory.Quantity -= DecreasedQuantity;
            context.SaveChanges();
        }

        public List<Product> GetAllByCategoryID(int id)
        {
            return context.Products
                .Include(p => p.Discount)
                .Include(p => p.Partener)
                .Include(p => p.Product_Images)
                .Include(p => p.subcategory)
                .Include(p => p.Product_Category)
                .Include(p => p.Product_Inventory)
                .Where(p => p.CategoryID == id)
                .Where(p2 => p2.StatusApproval == ProductApprovelEnum.Approved.ToString())
                .ToList();

        }
        
        public List<Product> GetAllBySubCategoryID(int id)
        {
            return context.Products.Include(p => p.Discount)
                .Include(p => p.Partener)
                .Include(p => p.Product_Images)
                .Include(p => p.subcategory)
                .Include(p => p.Product_Category)
                .Include(p => p.Product_Inventory)
                .Where(p => p.subcategoryID == id)
                .Where(p2 => p2.StatusApproval == ProductApprovelEnum.Approved.ToString())
                .ToList();

        }
        public List<Product> GetPartnerProducts(int PartnerID)
        {
            return context.Products.Include(p => p.Discount)
                .Include(p => p.Partener)
                .Include(p => p.Product_Images)
                .Include(p => p.subcategory)
                .Include(p => p.Product_Category)
                .Include(p => p.Product_Inventory)
                .Where(p => p.PartenerID == PartnerID)
                .ToList();
        }
        public List<Product> GetPartnerProductsByCategoryID(int PartnerID, int CategoryID)
        {
            return context.Products.Include(p => p.Discount)
                .Include(p => p.Partener)
                .Include(p => p.Product_Images)
                .Include(p => p.subcategory)
                .Include(p => p.Product_Category)
                .Include(p => p.Product_Inventory)
                //.Where(p2 => p2.StatusApproval == ProductApprovelEnum.Approved.ToString())
                .Where(p => p.PartenerID == PartnerID)
                .Where(p => p.CategoryID == CategoryID)
                .Where(p => p.DeletedAt == null).ToList();
        }
        public List<Product> GetPartnerProductsBySubCategoryID(int PartnerID, int SubCategoryID)
        {
            return context.Products.Include(p => p.Discount)
                .Include(p => p.Partener)
                .Include(p => p.Product_Images)
                .Include(p => p.subcategory)
                .Include(p => p.Product_Category)
                .Include(p => p.Product_Inventory)
                //.Where(p2 => p2.StatusApproval == ProductApprovelEnum.Approved.ToString())
                .Where(p => p.PartenerID == PartnerID)
                .Where(p => p.subcategoryID == SubCategoryID)
                .Where(p => p.DeletedAt == null).ToList();
        }
        public List<Product> GetAllWithInclude()
        {
            return context.Products.Include(p=>p.Discount)
                .Include(p => p.Partener)
                .Include(p => p.Product_Images)
                .Include(p => p.subcategory)
                .Include(p => p.Product_Category)
                .Include(p => p.Product_Inventory)
                .Where(p2 => p2.StatusApproval == ProductApprovelEnum.Approved.ToString())
                .Where(p => p.DeletedAt == null).ToList();
        }
        public List<string> GetImages(int id)
        {
            Product _product = context.Products.Include(p => p.Product_Images).FirstOrDefault(p => p.ID == id);

            List<string> _Images = _product.Product_Images.Select(p => p.ImageFileName).ToList();
            return _Images;
        }
        public List<Product_Images> GetImagesByProductID(int id)
        {
            List<Product_Images> AllImages = context.product_Images.Where(p=>p.ProductID == id).ToList();
            return AllImages;
        }

        
        public Product Get(int Id)
        {
            return context.Products.Include(p => p.Product_Images).FirstOrDefault(p=>p.ID == Id);
        } 
        public void Create(Product Product)
        {
            Product.CreatedAt = DateTime.Now;
            context.Products.Add(Product);
            context.SaveChanges();
        }
        public void Delete(int Id)
        {
            context.Products.Remove(Get(Id));
            context.SaveChanges();
        }
        public int Deletee(int Id)
        {
            Product product = Get(Id);
            if (product != null)
            {
                context.Products.Remove(product);
                return context.SaveChanges();
            }
            return 0;
           
        }
        public void Update(int Id, Product Product)
        {
            Product OldProduct = Get(Id);

            OldProduct.Name = Product.Name;
            OldProduct.Name_Ar = Product.Name_Ar;
            OldProduct.Description = Product.Description;
            OldProduct.Description_Ar = Product.Description_Ar;
            OldProduct.IsAvailable = Product.IsAvailable;
            OldProduct.Price = Product.Price;
            OldProduct.StatusApproval = Product.StatusApproval;

            OldProduct.CategoryID = Product.CategoryID;
            OldProduct.DiscountID = Product.DiscountID;
            OldProduct.PartenerID = Product.PartenerID;
            OldProduct.subcategoryID = Product.subcategoryID;
            OldProduct.InventoryID = Product.InventoryID;

            OldProduct.UpdatedAt = DateTime.Now;
            context.SaveChanges();
        }

        public void IsDiscountFinish()
        {
            List<Discount> discounts = context.Discounts.ToList();
            foreach (Discount discount in discounts)
            {
                if (discount.EndTime < discount.StartTime)
                {
                    List<Product> products = context.Products.Where(p => p.DiscountID == discount.ID).ToList();
                    foreach (Product product in products)
                    {
                        product.DiscountID = null;
                        context.SaveChanges();
                    }
                    context.Discounts.Remove(discount);
                    context.SaveChanges();
                }
            }
            
        }
        public Product GetIncludeById(int Id)
        {
            return context.Products.Include(e => e.Product_Category).Include(c => c.subcategory)
                .Include(r => r.Product_Inventory).Include(pr => pr.Discount).Include(im=>im.Product_Images)
                .Include(par => par.Partener)
                .Where(p2 => p2.StatusApproval == ProductApprovelEnum.Approved.ToString())
                .FirstOrDefault(y => y.ID == Id);
        }
        public Product GetUnApprovedAndApprovedById(int Id)
        {
            return context.Products.Include(e => e.Product_Category).Include(c => c.subcategory)
                .Include(r => r.Product_Inventory).Include(pr => pr.Discount).Include(im => im.Product_Images)
                .Include(par => par.Partener)
                .FirstOrDefault(y => y.ID == Id);
        }
        public List<Product> GetAllwithCategoryID(int id)
        {
            return context.Products.Where(e => e.CategoryID == id).ToList();
        }
        //List<Product> GetAllwithCategoryID(int id)
        //{

        //}
        public List<Product> GetNotApprovedByPartner(int partnerId)
        {
            return context.Products.Include(p => p.Discount)
                .Include(p => p.Partener)
                .Include(p => p.Product_Images)
                .Include(p => p.subcategory)
                .Include(p => p.Product_Category)
                .Include(p => p.Product_Inventory)
                .Where(p => p.DeletedAt == null)
                .Where(p => p.PartenerID == partnerId)
                .Where(p2 => p2.StatusApproval != ProductApprovelEnum.Approved.ToString()).ToList();
        }

        public List<Product> GetIncludeByName(string Name)
        {
            return context.Products.Include(e => e.Product_Category).Include(c => c.subcategory)
                .Include(r => r.Product_Inventory).Include(pr => pr.Discount).Include(im => im.Product_Images)
                .Include(par => par.Partener)
                .Where(p2 => p2.StatusApproval == ProductApprovelEnum.Approved.ToString())
                .Where(proud => EF.Functions.Like(proud.Name, '%'+Name+'%')).ToList();
        }

        public List<Product> GetIncludeByArabicName(string Name)
        {
            return context.Products.Include(e => e.Product_Category).Include(c => c.subcategory)
               .Include(r => r.Product_Inventory).Include(pr => pr.Discount).Include(im => im.Product_Images)
               .Include(par => par.Partener)
               .Where(p2 => p2.StatusApproval == ProductApprovelEnum.Approved.ToString())
               .Where(proud => EF.Functions.Like(proud.Name_Ar, '%' + Name + '%')).ToList();
        }
    }
}

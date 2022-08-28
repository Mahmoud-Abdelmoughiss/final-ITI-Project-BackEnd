using EcommerseApplication.DTO;
using EcommerseApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerseApplication.Respository
{
    public class DiscountRepository : IDiscount

    {
        private readonly Context context;

        public DiscountRepository(Context _context)
        {
            context = _context;
        }
        public void AddnewDiscount(Discount newdiscount)
        {
            context.Add(newdiscount);
            context.SaveChanges();
        }
        public List<Discount> GetAllDiscountByPartener(int PartnerID)
        {
            //List<Product> Products = context.Products.Where(p=>p.PartenerID == PartnerID).ToList();
            //List<Discount> discounts = context.Discounts.Include(p => p.Products).Where(d=>d.Products.Any(a=>a.PartenerID == PartnerID)).ToList();
            //List<Discount> discounts2 = context.Discounts.Where(d=>d.Products.Any(a=>a.PartenerID == PartnerID)).ToList();
            List<Discount> ListDiscount = context.Discounts.Where(d => d.PartnerId == PartnerID).ToList();
            return ListDiscount;
            //List<Discount> discounts = Products.Where(d=>d.Products.D)
        }
        public void AddnewDiscountt(DiscountDTO NewDiscount)
        {
            Discount discount = new Discount();
            discount.Name = NewDiscount.Name;
            discount.Description_Ar=NewDiscount.Description_Ar;
            discount.Name_Ar = NewDiscount.Name_Ar;
            discount.Description = NewDiscount.Description;
            discount.Descount_Persent = NewDiscount.Descount_Persent;
            discount.CreatedAt = DateTime.Now;
            discount.EndTime = NewDiscount.EndTime;
            discount.StartTime = NewDiscount.StartTime;
            discount.Active = NewDiscount.Active;
            context.Add(discount);
            context.SaveChanges();
        }
        public List< Discount> getDiscount()
        {
            List<Discount> discounts = context.Discounts.ToList();
            return discounts;

        }
        public Discount getDiscountById(int Id)
        {
            Discount discount = context.Discounts.FirstOrDefault(d => d.ID == Id);
            return discount;

        }

        
       
        public int  AssignDiscount(DiscountIDPartnerIDProductIDDTO AssignNewDiscount)
        {
            Product product = context.Products.FirstOrDefault(p => p.ID == AssignNewDiscount.ProductID);
               
           
                product.DiscountID = AssignNewDiscount.DiscountId;
                //int price = product.Price;
               // Discount
               // discount = context.Discounts.FirstOrDefault(d => d.ID == AssignNewDiscount.DiscountId);
               // decimal DiscountPersent = discount.Descount_Persent;
               // p
                //if (DiscountPersent > 0)
                //{
                //    int priceAfetrDiscount = price - (int)((price * DiscountPersent) / 100);
                //    product.Price = priceAfetrDiscount;
                //    context.SaveChanges();
                //    return 1;
                //}
                context.SaveChanges();
                return 2; 
            
            
        }
        public int DeleteDiscount(int Id)
        {
            Discount discount=context.Discounts.FirstOrDefault(p=>p.ID==Id);
            if(discount!=null)
            {
                context.Discounts.Remove(discount);
                return  context.SaveChanges();
                
            }
            return 0;
        }

        public int DeleteForPartener(int DiscountID , int PartenerID)
        {
            List<Product> PartnerProducts = context.Products.Where(p => p.PartenerID == PartenerID)
                                                            .Where(d=>d.DiscountID == DiscountID).ToList();
            List<Product> AllProducts = context.Products.Where(d => d.DiscountID == DiscountID).ToList();
            if (AllProducts.Count > PartnerProducts.Count)
                return -1;

            if (PartnerProducts.Count > 0)
            {
                for (int i = 0; i < PartnerProducts.Count; i++)
                {
                    PartnerProducts[i].DiscountID = null;
                }
            }
            Discount discount = context.Discounts.FirstOrDefault(p => p.ID == DiscountID);
            context.Discounts.Remove(discount);
            context.SaveChanges();
            return 1;
        }
        public int UpdateDiscount(int Id , DiscountDTO NewDiscount)
        {

            Discount discount = context.Discounts.FirstOrDefault(d => d.ID == Id);
            if (discount != null)
            {
                discount.Name = NewDiscount.Name;
                discount.Description = NewDiscount.Description;
                discount.Description_Ar = NewDiscount.Description_Ar;
                discount.Name_Ar = NewDiscount.Name_Ar;
                discount.Descount_Persent = NewDiscount.Descount_Persent;
                discount.UpdatedAt = DateTime.Now;
                discount.EndTime = NewDiscount.EndTime;
                discount.StartTime = NewDiscount.StartTime;
                discount.Active = NewDiscount.Active;
                return context.SaveChanges();
            }
            return 0;
        }

        public List<Discount> GetAllByPartener(int PartnerID)
        {
            //List<Product> Products = context.Products.Where(p=>p.PartenerID == PartnerID).ToList();
            //List<Discount> discounts = context.Discounts.Include(p => p.Products).Where(d=>d.Products.Any(a=>a.PartenerID == PartnerID)).ToList();
            //List<Discount> discounts2 = context.Discounts.Where(d=>d.Products.Any(a=>a.PartenerID == PartnerID)).ToList();
            var discountsTemp = context.Discounts.Select(d => new
            {
                d,
                prods = d.Products.Where(p => p.PartenerID == PartnerID).ToList()
            }).ToList();
            foreach (var item in discountsTemp)
            {
                item.d.Products = item.prods;
            }
            List<Discount> discounts = discountsTemp.Where(x=>x.prods.Count != 0).Select(x => x.d).ToList();
            return discounts;
            //List<Discount> discounts = Products.Where(d=>d.Products.D)
        }
    }
}

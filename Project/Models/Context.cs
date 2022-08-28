using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace EcommerseApplication.Models
{
    public class Context : IdentityDbContext<AppUser>
    {
        public Context(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        /*  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          {
              optionsBuilder.UseSqlServer(@"Data source =.;Initial Catalog =EcommerseFinal;Integrated security=true");
          }*/
        public DbSet<Product> Products { get; set; }//
        public DbSet<User> users { get; set; }//
        public DbSet<Order_Details>order_Details{get;set;}//
        public DbSet<Order_Items> order_items { get; set; }//
        public DbSet<Product_Category> Product_Categorys { get; set; }//
        public DbSet<Product_Images> product_Images { get; set; }//
        public DbSet<Product_Inventory> Product_Inventorys { get; set; }//
        public DbSet<Discount> Discounts { get; set; }//
        public DbSet<Payment_Details> Payment_Details { get; set; }//
        public DbSet<Shopping_Session> Shopping_Sessions { get; set; }//
        public DbSet<Cart_Item> Cart_Items { get; set; }//
        public DbSet<User_address> User_Addresses { get; set; }//
        public DbSet<User_Payement> User_Payements { get; set; }
        public DbSet<Partener> Parteners { get; set; }//
        public DbSet<subCategory> subCategories { get; set; }//
        public DbSet<Shipper> shippers { get; set; }//
        public DbSet<feedback> feedbacks { get; set; }//
        public DbSet<shippingDetails> shippingDetails { get; set; }//

        public DbSet<Requests> Requests { get; set; }//

        public DbSet<ShipperRequest> shipperRequests { get; set; }




















    }
}

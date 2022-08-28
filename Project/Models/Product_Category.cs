namespace EcommerseApplication.Models
{
    public class Product_Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Name_Ar { get; set; }
        public string Description { get; set; }
        public string? Description_Ar { get; set; }


        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }


        public virtual ICollection<Product>? Products { get; set; }
        public List<subCategory>? SubCategories { get; set; }
    }
}

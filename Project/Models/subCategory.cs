using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerseApplication.Models
{
    public class subCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? arabicName { get; set; }
        public string Description { get; set; }
        public string? arabicDescription { get; set; }
        public string image { get; set; }
        [ForeignKey("category")]
        public int CategoryId { get; set; }
        public Product_Category? category{get;set;}
        public List<Product>? Products { get; set; }
    }
}

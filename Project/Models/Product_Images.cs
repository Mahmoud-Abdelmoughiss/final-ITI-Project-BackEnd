using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerseApplication.Models
{
    public class Product_Images
    {
        public int Id { get; set; }
        public string ImageFileName { get; set; }


        [ForeignKey("Product")]
        public int ProductID { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Product? Product { get; set; }
    }
}

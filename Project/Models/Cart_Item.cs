using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerseApplication.Models
{
    public class Cart_Item
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }


        [ForeignKey("shopping_Session")]
        public int SessionId { get; set; } 
        [ForeignKey("product")]
        public int ProductId { get; set; }


        public Shopping_Session? shopping_Session { get; set; }
        public Product? product { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerseApplication.Models
{
    public class feedback
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("user")]
        public int UserID { get; set; }
        [ForeignKey("Product")]
        public int productID { get; set; }
        [ForeignKey("order_Details")]
        public int OrderID { get; set; }
        public string Comment { get; set; }
       public Decimal Rate { get; set; }
        Product? Product { get; set; }
        Order_Details? order_Details { get; set; }
        public User? user { get; set; }
    }
}

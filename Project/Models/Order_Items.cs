using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerseApplication.Models
{
    public class Order_Items
    {
        public int ID { get; set; }
        public int Quantity { get; set; }


        [ForeignKey("Order_Detail")]
        public int? OrderID { get; set; }
        [ForeignKey("Product")]
        public int? ProductID { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Order_Details? Order_Detail { get; set; }
        public virtual Product? Product { get; set; }
    }
}

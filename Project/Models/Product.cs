using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerseApplication.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Name_Ar { get; set; }
        public string Description { get; set; }
        public string? Description_Ar { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }

        public string StatusApproval { get; set; }

        [ForeignKey("Product_Category")]
        public int? CategoryID { get; set; }
        [ForeignKey("Product_Inventory")]
        public int? InventoryID { get; set; }
        [ForeignKey("Discount")]
        public int? DiscountID { get; set; }
        [ForeignKey("Partener")]
        public int? PartenerID { get; set; }
        [ForeignKey("subcategory")]
        public int? subcategoryID { get; set; }


        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }


        public virtual Product_Category? Product_Category { get; set; }
        public virtual Product_Inventory Product_Inventory { get; set; }
        public virtual ICollection<Product_Images>? Product_Images { get; set; }

       public virtual Partener? Partener { get; set; }
        public virtual Discount? Discount { get; set; }

        public subCategory? subcategory { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;


namespace EcommerseApplication.DTO
{
    public class ProductResponseDTO
    {
        
        public int ID { get; set; }
        public string Name { get; set; }
        public string Name_Ar { get; set; }
        public string Description { get; set; }
        public string Description_Ar { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public decimal? Discount { get; set; }
        public bool IsAvailable { get; set; }
        public string StatusApproval { get; set; }

        public List<string> Images { get; set; }
        public string PartenerName { get; set; }
        public string CategoryName { get; set; }
        public string subcategoryName { get; set; }
        public int? CategoryID { get; set; }
        public int? subcategoryID { get; set; }
    }
}

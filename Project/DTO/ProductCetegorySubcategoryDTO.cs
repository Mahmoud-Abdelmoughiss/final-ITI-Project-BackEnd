using EcommerseApplication.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace EcommerseApplication.DTO
{
    public class ProductCetegorySubcategoryDTO
    {
        [Required]
        public string Name { get; set; }
        public string? Name_Ar { get; set; }
        [Required]
        public string Description { get; set; }
        public string? Description_Ar { get; set; }

        [Required]
        public int Price { get; set; }
        [Required]
        public int? CategoryID { get; set; }
        public bool IsAvailable { get; set; }
        [Required]
        public int? subcategoryID { get; set; }
        [Required]
        public int Quantity { get; set; }

        //[AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        //public IFormFileCollection? ImageFiles { get; set; }
        //public byte[]? ImageFiles { get; set; }
    }
}

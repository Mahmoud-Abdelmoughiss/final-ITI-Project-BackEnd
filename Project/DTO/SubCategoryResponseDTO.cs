using EcommerseApplication.Models;

namespace EcommerseApplication.DTO
{
    public class SubCategoryResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? arabicName { get; set; }
        public string? arabicDescription { get; set; }
        public string Image { get; set; }
        public string CategoryName { get; set; }
        public int categoryId { get; set; }
    }
}

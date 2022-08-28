namespace EcommerseApplication.DTO
{
    public class CategoryResponseDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Name_Ar { get; set; }
        public string Description { get; set; }
        public string? Description_Ar { get; set; }
        public List<SubCategoryResponseDTO> SubCategories { get; set; }
    }
}

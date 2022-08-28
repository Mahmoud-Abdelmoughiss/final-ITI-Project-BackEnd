namespace EcommerseApplication.DTO
{
    public class categoryDTOO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Name_Ar { get; set; }
        public string Description { get; set; }
        public string? Description_Ar { get; set; }
        public List<int> ? ListsubcategoryId { get; set; }
    }
}

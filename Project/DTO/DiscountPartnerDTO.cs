namespace EcommerseApplication.DTO
{
    public class DiscountPartnerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Name_Ar { get; set; }
        public string? Description_Ar { get; set; }
        public decimal Descount_Persent { get; set; }
        public bool Active { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public List<DiscountProductDTO> Products { get; set; }

    }
}

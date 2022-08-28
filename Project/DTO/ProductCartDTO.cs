namespace EcommerseApplication.DTO
{
    public class ProductCartDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public string arabicName { get; set; }
        public string arabicDiscription { get; set; }
        public string? Image { get; set; }
        public int? Price { get; set; }
        public decimal? Descount_Persent { get; set; }
        public int? QuantityOrdered { get; set; }
        public int? QuantityAvailable { get; set; }
        public string? categoryName { get; set; }
        public string? subCategoryName { get; set; }
        

    }
}

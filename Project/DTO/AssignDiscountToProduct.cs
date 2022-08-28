namespace EcommerseApplication.DTO
{
    public class AssignDiscountToProduct
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Descount_Persent { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}

namespace EcommerseApplication.Models
{
    public class Discount
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Name_Ar { get; set; }
        public string Description { get; set; }
        public string? Description_Ar { get; set; }
        public decimal Descount_Persent { get; set; }
        public bool Active { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? PartnerId { get; set; }


        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }


        public virtual ICollection<Product>? Products { get; set; }
    }
}

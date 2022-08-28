using System.ComponentModel.DataAnnotations;

namespace EcommerseApplication.DTO
{
    public class DiscountDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string? Name_Ar { get; set; }
        [Required]
        public decimal Descount_Persent { get; set; }
        public string? Description_Ar { get; set; }
        [Required]
        public bool Active { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }





    }
}

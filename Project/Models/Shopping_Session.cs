using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerseApplication.Models
{
    public class Shopping_Session
    {
        public int Id { get; set; }
        public Decimal Total { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        [ForeignKey("user")]
        public int? UserID { get; set; }
        public virtual User? user{ get; set; }
    }
}

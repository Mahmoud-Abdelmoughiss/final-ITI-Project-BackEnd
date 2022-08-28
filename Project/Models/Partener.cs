using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerseApplication.Models
{
    public class Partener
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int? userID { get; set; }
        public string Name { get; set; }
        public string? Type { get; set; }
        public int? numberOfBranches { get; set; }
        [ForeignKey("identity")]
        public string IdentityId { set; get; }
        public AppUser identity { get; set; }
        public User? User { get; set; }
        public int? addressID;

        public List<Product>? Products { get; set; }
    }
}

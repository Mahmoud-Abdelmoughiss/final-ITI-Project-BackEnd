using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerseApplication.Models
{
    public class Requests
    {
        [Key]
        public int RequestId { get; set; }
        public string Name { get; set; }
        public int? numberOfBranches { get; set; }

        public string RequestType { get; set; }
        [ForeignKey("identity")]
        public string IdentityId { set; get; }
        public AppUser identity { get; set; }
         

    }
}

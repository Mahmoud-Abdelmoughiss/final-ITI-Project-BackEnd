using System.ComponentModel.DataAnnotations;

namespace EcommerseApplication.DTO
{
    public class RequestDto
    {
        [Required]
        [MinLength(1)]
        public string Name{ get; set; }
        [Required]
        public int numberOfBranches { get; set; }
    }
}

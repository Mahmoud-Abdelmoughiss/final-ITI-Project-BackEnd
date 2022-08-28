using System.ComponentModel.DataAnnotations;

namespace EcommerseApplication.ViewModels
{
    public class AssignRolesByEmail
    {
        [EmailAddress]
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Role Name is required")]
        public string RoleName { get; set; }
    }
}

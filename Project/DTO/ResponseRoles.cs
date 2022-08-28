using EcommerseApplication.Models;

namespace EcommerseApplication.DTO
{
    public class ResponseRoles
    {
        public List<AppUser> Admins { get; set; }
        public List<AppUser> SAdmins { get; set; }
        public List<AppUser> Partners { get; set; }
        public List<AppUser> Shippers { get; set; }

    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerseApplication.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }//real user Name
        public string? UserNameAR { get; set; }
 
        public string? AcountID { get; set; }//linked to user Account
        //public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? FirstNameAR { get; set; }
        public string? LastName { get; set; }
        public string? LastNameAR { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        [Column(TypeName = "date")]
        public DateTime? birthDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        [ForeignKey("Identity")]
        public string? IdentityId { get; set; } //reference to AppUser and maps to it
        public AppUser? Identity { get; set; }
        public string? StripeTokenID { get; set; }

        // public virtual List<Product>? Products { get; set; }
        public virtual List<Shopping_Session>? Shopping_Sessions { get; set; }
        public virtual List<User_address>? User_Address { get; set; }
        public virtual List<User_Payement>? User_Pyment { get; set; }
        public List<Order_Details>? Order_Details { get; set; }





    }
}

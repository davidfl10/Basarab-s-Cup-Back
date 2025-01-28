using System.ComponentModel.DataAnnotations;

namespace finalexam_back.Models
{
    public class User
    {
        public int? Id { get; set; }
        [StringLength(30, ErrorMessage = "Your First Name must contain less than 30 characters")]
        public string? FirstName { get; set; }
        [StringLength(30, ErrorMessage = "Your Last Name must contain less than 30 characters")]
        public string? LastName { get; set; }
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Your Phone Number must contain at least 8 characters")]
        public string? Phone { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Your Password must contain at least 8 characters")]
        public string Password { get; set; }
        public virtual ICollection<Cart>? Carts { get; set; }
        public string? TeamCode { get; set; }
        public virtual Team? Team { get; set; }
    }
}

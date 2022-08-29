using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class UserModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
        public string? Email { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AppUser class
public class AppUser : IdentityUser
{
    [Required]
    [MinLength(3)]
    public string Login { get; set; }
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
    [Required]
    public string ConfirmPassword { get; set; }


}


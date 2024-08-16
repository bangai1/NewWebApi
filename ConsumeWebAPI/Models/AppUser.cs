using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ConsumeWebAPI.Models
{
    public class AppUser:IdentityUser
    {
        [StringLength(100)]
        [MaxLength(100)]
        [Required]
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Address { get; set; }

        public string? Id { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }



    }
}

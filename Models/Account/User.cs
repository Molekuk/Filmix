using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Filmix.Models.AccountModels
{
    public class User:IdentityUser
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string DateBirth { get; set; }
    }
}

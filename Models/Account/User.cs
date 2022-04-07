using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Filmix.Models.AccountModels
{
    public class User:IdentityUser
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        

    }
}

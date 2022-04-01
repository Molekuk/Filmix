﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Filmix.Models.Account
{
    public class User:IdentityUser
    {
        [Required]
        [MaxLength(10)]
        public string DateBirth { get; set; }
    }
}

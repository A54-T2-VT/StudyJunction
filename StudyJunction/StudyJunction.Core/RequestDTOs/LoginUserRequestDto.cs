﻿using StudyJunction.Infrastructure.Constants;
using System.ComponentModel.DataAnnotations;

namespace StudyJunction.Core.RequestDTOs
{
    public class LoginUserRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_mk1.Models
{
    public class UserSignupModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage="passwords don't match")]
        public string ConfirmPassword { get; set; }
    }
}

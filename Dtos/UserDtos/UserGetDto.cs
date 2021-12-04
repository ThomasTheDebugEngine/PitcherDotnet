using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_mk1.Dtos
{
    public class UserGetDto
    {
        [Required]
        [MaxLength(250)]
        public string UserId { get; set; }

        [Required]
        [MaxLength(250)]
        public string UserName { get; set; }

        [Required]
        public bool IsContractor { get; set; }
    }
}

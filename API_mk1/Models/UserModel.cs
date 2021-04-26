using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API_mk1.Models.User
{
    [Table("Users")]
    [Index(nameof(UserId), IsUnique = true)]
    public class UserModel
    {
        [Key]
        public long DbId { get; set; }
        
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

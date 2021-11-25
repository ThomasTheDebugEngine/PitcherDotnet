using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API_mk1.Models.Project;

namespace API_mk1.Models.User
{
    [Table("Users_Table")]
    [Index(nameof(UserId), IsUnique = true)]
    public class UserModel
    {
        [Key]
        public long DbId { get; set; } //might not need this

        [Required]
        [MaxLength(250)]
        public string UserId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        //[Required]
        //[MaxLength(250)]
        //public string PasswordHash { get; set; }
        
        [Required]
        public bool IsContractor { get; set; }

        //nav prop
        public List<ProjectModel> Projects { get; set; } //figure out if I have to have the entire object in the list
    }
}

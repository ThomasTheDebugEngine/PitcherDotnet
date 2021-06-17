using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using API_mk1.Models.User;

namespace API_mk1.Models.Project
{
    [Table("ProjectOwners")]
    public class ProjectModel
    {
        [Key]
        public long DbId { get; set; }

        [Required]
        public string ProjectId { get; set; } //generated at creation (name + title + time)

        [Required]
        public string OwnerId { get; set; } //foreign key to user table

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public long CreatedAtUnix { get; set; }

        //nav prop
        public virtual UserModel UserModel { get; set; }
    }
}

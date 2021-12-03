using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using API_mk1.Models;

namespace API_mk1.Models
{
    [Table("Projects_Table")]
    public class ProjectModel
    {
        [Key]
        public long DbId { get; set; } // might not need

        [Required]
        [MaxLength(250)]
        public string ProjectId { get; set; } //generated at creation (name + title + time), foreign key to user table

        [Required]
        [MaxLength(250)]
        public string OwnerId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        [MaxLength(250)]
        public string Body { get; set; }

        [Required]
        public long CreatedAtUnix { get; set; }

        public long likeNumber { get; set; }

        //nav prop
        [ForeignKey("ProjectId")]
        public virtual UserModel User { get; set; }
    }
}

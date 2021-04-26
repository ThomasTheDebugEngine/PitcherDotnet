using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_mk1.Models
{
    [Table("ProjectOwners")]
    public class ProjectOwnerModel
    {
        [Key]
        public long DbId { get; set; }

        [Required]
        public string ProjectId { get; set; } //generated at creation (add name + title + time ? to SHA2)

        [Required]
        public string OwnerId { get; set; } //foreign key that relates to user table

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public long CreatedAtUnix { get; set; }
    }
}

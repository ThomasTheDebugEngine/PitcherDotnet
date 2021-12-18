using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API_mk1.Models
{
    [Table("Like_Table")]
    [Index(nameof(ProjectId), IsUnique = true)]
    public class LikeModel
    {
        [Key]
        public long DbId { get; set; }

        public string ProjectId { get; set; }

        public string LikedBy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API_mk1.Models
{
    [Table("Stars_Table")]
    [Index(nameof(ProjectId), IsUnique = true)]
    public class StarModel
    {
        [Key]
        public long DbId { get; set; }

        public string ProjectId { get; set; }

        public string StarredBy { get; set; }
    }
}

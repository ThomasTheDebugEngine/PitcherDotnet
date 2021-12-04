using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_mk1.Dtos
{
    public class ProjectGetDto
    {
        [Required]
        [MaxLength(250)]
        public string ProjectId { get; set; }

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

        public long LikeNumber { get; set; }
    }
}

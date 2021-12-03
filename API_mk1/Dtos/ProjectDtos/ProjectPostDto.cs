using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_mk1.Dtos
{
    public class ProjectPostDto
    {
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
        [MaxLength(36)]
        public string OwnerId { get; set; }
    }
}

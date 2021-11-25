using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_mk1.Models.Funder
{
    public class FunderModel
    {
        [Key]
        public int dbId;

        [Required]
        public string FunderId; //need to set as alternate key

        [Required]
        public string ProjectOwnerId;

        [Required]
        public int FundAmount;
    }
}

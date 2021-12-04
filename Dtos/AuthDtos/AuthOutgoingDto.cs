using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_mk1.Dtos
{
    public class AuthOutgoingDto
    {
        public string id { get; set; }

        public string userName { get; set; }

        public bool emailConfirmed { get; set; }

        public int accesFailedCount { get; set; }
    }
}

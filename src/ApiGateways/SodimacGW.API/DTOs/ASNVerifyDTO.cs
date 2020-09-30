using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SodimacGW.API.DTOs
{
    public class ASNVerifyDTO
    {
        public int Id { get; set; }
        public string CodASN { get; set; }
        public string Details { get; set; }
        public bool Available { get; set; }
    }
}

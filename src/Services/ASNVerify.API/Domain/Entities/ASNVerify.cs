﻿namespace ASNVerify.API.Domain.Entities
{
    public class ASNVerify
    {
        public int Id { get; set; }
        public string CodASN { get; set; }
        public string Details { get; set; }
        public bool Available { get; set; }
    }
}

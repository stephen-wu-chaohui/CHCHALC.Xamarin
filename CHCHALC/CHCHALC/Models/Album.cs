using System;
using System.Collections.Generic;

namespace CHCHALC.Models
{
    public class Album : Entity
    {
        public Int32 GroupId { get; set; }

        public string Image { get; set; }
        public string Name { get; set; }
        public string MissionStatement { get; set; }

        public List<Post> Posts { get; set; }
    }
}
using System;

namespace CHCHALC.Models
{
    public class Participate : Entity
    {
        public Int32 GroupId { get; set; }

        public Int32 DiscipleId { get; set; }
        public String Title { get; set; }
        public String Statement { get; set; }

        public DateTime From { get; set; }
        public DateTime? To { get; set; }
    }
}

using System;

namespace CHCHALC.Models
{
    public class Entity : ICloneable
    {
        public Int32 Id { get; set; }
        public Int32 LastUpdated { get; set; }
        public bool Deleted { get; set; }

        public string Keywords { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

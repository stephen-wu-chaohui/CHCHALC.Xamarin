using System;
using System.Collections.Generic;

namespace CHCHALC.Models
{
    public class Group : Entity
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Visibility { get; set; }      // Any/Church
        public string MissionStatement { get; set; }

        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public List<Album> Albums { get; set; }
        public bool IsAdmin { get; set; }
    }
}

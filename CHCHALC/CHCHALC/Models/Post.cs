using System;

namespace CHCHALC.Models
{
    public class Post : Entity
    {
        public Int32 AlbumId { get; set; }

        public string Image { get; set; }
        public string Name { get; set; }
        public string Visibility { get; set; }      // Any/Church/Member
        public string Type { get; set; }    // Sermon | Fellowship

        public string MissionStatement { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
    }
}

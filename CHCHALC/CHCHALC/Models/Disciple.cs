
namespace CHCHALC.Models
{
    public class Disciple : Entity
    {
        public string FormalName { get; set; }
        public string PreferName { get; set; }
        public string Gender { get; set; }              // Male, Female, Unknown
        public string PersonalStatement { get; set; }

        public string PhoneNumber { get; set; }
        public string BirthDate { get; set; }           // ISO-8601
        public string Visibility { get; set; }          // FormalName;Gender;BirthDate

        public string Token { get; set; }
        public string Title { get; set; }               // only used in getGroupMembers
    }
}

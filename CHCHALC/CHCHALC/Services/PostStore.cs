using CHCHALC.Models;

namespace CHCHALC.Services
{
    public class PostStore : AzureDataStore<Post>
    {
        public PostStore() : base("Events") { }
    }

}

using CHCHALC.Models;

namespace CHCHALC.Services
{
    public class AlbumStore : AzureDataStore<Disciple>
    {
        public AlbumStore() : base("Albums") { }
    }

}

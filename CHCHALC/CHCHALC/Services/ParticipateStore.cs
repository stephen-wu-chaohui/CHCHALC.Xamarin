using CHCHALC.Models;

namespace CHCHALC.Services
{
    public class ParticipateStore : AzureDataStore<Participate>
    {
        public ParticipateStore() : base("Participates") { }
    }

}

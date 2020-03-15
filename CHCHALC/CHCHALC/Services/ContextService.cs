using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CHCHALC.Misc;
using CHCHALC.Models;
using Xamarin.Forms;

namespace CHCHALC.Services
{
    public class ContextService : NotifyPropertyChanged, IContextService
    {
        private Group currentGroup;
        public Group CurrentGroup {
            get { return currentGroup; }
            set { SetProperty(ref currentGroup, value); }
        }

        private List<Disciple> candidates;
        public List<Disciple> Candidates {
            get { return candidates; }
            set { SetProperty(ref candidates, value); }
        }

        private Disciple storedDisciple;
        public Disciple StoredDisciple {
            get {
                return storedDisciple;
            }
            set {
                SetProperty(ref storedDisciple, value);
                storedDisciple.WriteSettings("StoredDisciple");
                if (storedDisciple != null) {
                    if (Candidates != null) {
                        int pos = Candidates.IndexOf(c => c.Id == storedDisciple.Id);
                        if (pos == -1)
                            Candidates.Add(storedDisciple);
                        else
                            Candidates[pos] = storedDisciple;
                        Candidates.ToArray().WriteSettings("Candidates");
                        Candidates = Candidates;
                    }
                    DependencyService.Get<IDataService>().SignUpAsync(storedDisciple);
                }
            }
        }

        private readonly IDataStore<Disciple> DiscipleDirectory = DependencyService.Get<IDataStore<Disciple>>();

        public ContextService()
        {
            currentGroup = Extensions.ReadSettings<Group>("CurrentGroup");
            Candidates = Extensions.ReadSettings<Disciple[]>("Candidates")?.ToList() ?? new List<Disciple>();
            storedDisciple = Extensions.ReadSettings<Disciple>("StoredDisciple");
        }

        public async Task<bool> LoginAsync()
        {
            if (StoredDisciple == null)
                return false;
            var logined = await DependencyService.Get<IDataService>().SignUpAsync(storedDisciple);
            return logined;
        }

        public async Task<bool> SaveChangesAsync(Disciple disciple, bool createNew)
        {
            if (disciple == null)
                return false;

            if (StoredDisciple != null && !createNew)
                disciple.Id = StoredDisciple.Id;

            if (disciple.Id == 0)
            {
                StoredDisciple = await DiscipleDirectory.AddItemAsync(disciple);
            }
            else
            {
                await DiscipleDirectory.UpdateItemAsync(disciple);
                StoredDisciple = disciple;
            }
            return true;
        }

        public bool Logout()
        {
            StoredDisciple = null;
            return true;
        }
    }
}

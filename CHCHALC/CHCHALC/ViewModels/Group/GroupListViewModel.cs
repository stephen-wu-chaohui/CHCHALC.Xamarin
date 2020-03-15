using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CHCHALC.Models;
using CHCHALC.Services;
using Xamarin.Forms;
using CHCHALC.Misc;

namespace CHCHALC.ViewModels
{
    public class GroupListViewModel : BaseViewModel
    {
        private IContextService ProfileService => DependencyService.Get<IContextService>();
        private IDataService dataService => DependencyService.Get<IDataService>();

        public ObservableCollection<Group> Groups { get; set; } = new ObservableCollection<Group>();

        public bool CanCreateGroups => (ProfileService?.StoredDisciple != null);
        public bool HasGroupParticipated => Groups.Count > 0;
        public bool NoGroupParticipated => Groups.Count == 0;

        public Command LoadCommand { get; }

        public GroupListViewModel()
        {
            Title = "Groups";

            LoadCommand = new Command(async () => await ExecuteLoadCommand());

            ProfileService.PropertyChanged += (object sender, PropertyChangedEventArgs e) => {
                if (e.PropertyName == "StoredDisciple") {
                    LoadCommand.Execute(null);
                }
            };

            MessagingCenter.Subscribe<Group>(this, "upsert", async group => {
                group = await dataService.UpsertAsync(group);

                Groups.Upsert(group);
                OnPropertyChanged("HasGroupParticipated");
                OnPropertyChanged("NoGroupParticipated");
            });

            MessagingCenter.Subscribe<Group>(this, "delete", group => {
                Groups.Delete(group);
                OnPropertyChanged("HasGroupParticipated");
                OnPropertyChanged("NoGroupParticipated");
            });

            MessagingCenter.Subscribe<Group>(this, "join", group => {
                Groups.Upsert(group);
                OnPropertyChanged("HasGroupParticipated");
                OnPropertyChanged("NoGroupParticipated");
            });

        }

        async Task ExecuteLoadCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (ProfileService?.StoredDisciple == null) {
                IsBusy = false;
                return;
            }

            try {
                var allGroups = await dataService.GetParticipatedGroups();
                Groups.Clear();
                allGroups.ToList().ForEach(r => Groups.Add(r));
                OnPropertyChanged("HasGroupParticipated");
                OnPropertyChanged("NoGroupParticipated");
            } catch (Exception ex) {
                Debug.WriteLine(ex);
            } finally {
                IsBusy = false;
            }
        }
    }
}

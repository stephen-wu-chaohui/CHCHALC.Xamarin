using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CHCHALC.Models;
using CHCHALC.Services;
using Xamarin.Forms;

namespace CHCHALC.ViewModels
{
    public class PublicGroupListViewModel : BaseViewModel
    {
        private readonly IContextService ProfileService = DependencyService.Get<IContextService>();
        private readonly IDataService DataService = DependencyService.Get<IDataService>();

        public ObservableCollection<Group> Groups { get; set; } = new ObservableCollection<Group>();

        public bool ChurchOnly => (ProfileService?.StoredDisciple != null);

        public bool NoGroupToJoin => !Groups.Any();

        public Command LoadCommand { get; }

        public PublicGroupListViewModel()
        {
            Title = "Groups";
            LoadCommand = new Command(async () => await ExecuteLoadCommand());

            LoadCommand.Execute(null);

            ProfileService.PropertyChanged += (object sender, PropertyChangedEventArgs e) => {
                if (e.PropertyName == "StoredDisciple") {
                    LoadCommand.Execute(null);
                }
            };
        }

        async Task ExecuteLoadCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try {
                var allGroups = await DataService.GetPublicGroups(ChurchOnly);
                Groups.Clear();
                allGroups.ToList().ForEach(r => Groups.Add(r));
                OnPropertyChanged("NoGroupToJoin");
            } catch (Exception ex) {
                Debug.WriteLine(ex);
            } finally {
                IsBusy = false;
            }
        }
    }
}

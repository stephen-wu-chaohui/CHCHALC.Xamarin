using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CHCHALC.Models;
using CHCHALC.Services;
using Xamarin.Forms;

namespace CHCHALC.ViewModels
{
    public class MemberListViewModel : BaseViewModel
    {
        private readonly IContextService ProfileService = DependencyService.Get<IContextService>();
        private readonly IDataService DataService = DependencyService.Get<IDataService>();

        public ObservableCollection<Disciple> Members { get; set; } = new ObservableCollection<Disciple>();

        public Group CurrentGroup { get; }

        public Command LoadCommand { get; set; }

        public MemberListViewModel(Group group)
        {
            Title = "Members";
            CurrentGroup = group;

            LoadCommand = new Command(async () => await ExecuteLoadCommand());
        }

        async Task ExecuteLoadCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (ProfileService.StoredDisciple == null || ProfileService.StoredDisciple.Id <= 0) {
                IsBusy = false;
                return;
            }

            try {
                IEnumerable<Disciple> members = await DataService.GetGroupMembers(CurrentGroup.Id);
                Members.Clear();
                members.ToList().ForEach(r => Members.Add(r));
            } catch (Exception ex) {
                Debug.WriteLine(ex);
            } finally {
                IsBusy = false;
            }
        }
    }
}

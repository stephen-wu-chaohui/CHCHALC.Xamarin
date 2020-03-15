using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using CHCHALC.Models;
using CHCHALC.Services;
using Xamarin.Forms;

namespace CHCHALC.ViewModels
{
    public class TestingViewModel : BaseViewModel
    {
        public Command LoadCommand { get; set; }
        public Command CreateCommand { get; set; }
        public Command LogoutCommand { get; set; }

        private string name;
        public string Name {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string nickName;
        public string NickName {
            get { return nickName; }
            set { SetProperty(ref nickName, value); }
        }

        public IContextService ProfileService = DependencyService.Get<IContextService>();

        public ObservableCollection<Disciple> Candidates { get; set; } = new ObservableCollection<Disciple>();

        public TestingViewModel()
        {
            Title = "Testing";
            LoadCommand = new Command(() => ExecuteLoadCommand());
            CreateCommand = new Command(async () => await ExecuteCreateCommand());
            LogoutCommand = new Command(() => ExecuteLogoutCommand());

            LoadCommand.Execute(null);

            ProfileService.PropertyChanged += (sender, e) => {
                if (e.PropertyName == "Candidates") {
                    LoadCommand.Execute(null);
                }
            };
        }

        void ExecuteLogoutCommand()
        {
            ProfileService.Logout();
        }

        internal void SetCurrent(Disciple profile)
        {
            ProfileService.StoredDisciple = profile;
        }

        async Task ExecuteCreateCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                var me = new Disciple
                {
                    FormalName = Name,
                    PreferName = NickName,
                };
                await ProfileService.SaveChangesAsync(me, true);
            } catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        void ExecuteLoadCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Candidates.Clear();
                var items = ProfileService.Candidates;
                foreach (var item in items)
                {
                    Candidates.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}

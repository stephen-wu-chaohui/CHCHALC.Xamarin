using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CHCHALC.Models;
using CHCHALC.Services;
using Xamarin.Forms;

namespace CHCHALC.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public Command ConfirmCommand { get; set; }
        public Command LoadCommand { get; set; }

        private string formalName;
        public string FormalName {
            get { return formalName; }
            set { SetProperty(ref formalName, value); }
        }

        private string preferName;
        public string PreferName {
            get { return preferName; }
            set { SetProperty(ref preferName, value); }
        }

        private string personalStatement;
        public string PersonalStatement {
            get { return personalStatement; }
            set { SetProperty(ref personalStatement, value); }
        }

        private string description;
        public string Description {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        public IContextService ProfileService = DependencyService.Get<IContextService>();

        public ProfileViewModel()
        {
            Title = "Profile";
            ConfirmCommand = new Command(async () => await ExecuteConfirmCommand());

            FormalName = ProfileService.StoredDisciple?.FormalName ?? "";
            PreferName = ProfileService.StoredDisciple?.PreferName ?? "";

            ProfileService.PropertyChanged += (sender, e) => {
                if (e.PropertyName == "StoredDisciple") {
                    FormalName = ProfileService.StoredDisciple?.FormalName ?? "";
                    PreferName = ProfileService.StoredDisciple?.PreferName ?? "";
                }
            };
        }

        async Task ExecuteConfirmCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                var me = new Disciple
                {
                    FormalName = FormalName,
                    PreferName = PreferName,
                    PersonalStatement = PersonalStatement,
                };
                await ProfileService.SaveChangesAsync(me, false);
            } catch (Exception ex)
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

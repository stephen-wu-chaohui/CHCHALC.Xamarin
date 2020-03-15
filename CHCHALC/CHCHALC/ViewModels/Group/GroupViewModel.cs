using Xamarin.Forms;
using CHCHALC.Models;
using CHCHALC.Misc;
using System;
using CHCHALC.Services;

namespace CHCHALC.ViewModels
{
    public class GroupViewModel : BaseViewModel
    {
        readonly IContextService contextService = DependencyService.Get<IContextService>();
        readonly IDataService dataService = DependencyService.Get<IDataService>();

        private string name;
        public string Name {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string image;
        public string Image {
            get { return image; }
            set { SetProperty(ref image, value); }
        }

        private string address;
        public string Address {
            get { return address; }
            set { SetProperty(ref address, value); }
        }

        private double latitude;
        public double Latitude {
            get { return latitude; }
            set { SetProperty(ref latitude, value); }
        }

        private double longitude;
        public double Longitude {
            get { return longitude; }
            set { SetProperty(ref longitude, value); }
        }

        private string missionStatement;
        public string MissionStatement {
            get { return missionStatement; }
            set { SetProperty(ref missionStatement, value); }
        }

        public Command SaveCommand { get; set; }
        public Command JoinCommand { get; set; }

        // IsJoinable

        public bool IsEditable { get; }
        public string EditingButtonText { get; }

        public bool IsJoinable { get; internal set; }
        public string JoiningButtonText { get; }

        public Group CurrentGroup { get; internal set; }

        public GroupViewModel(Group group, bool tojoin = false)
        {
            CurrentGroup = group;

            Title = "Group";
            IsJoinable = tojoin && contextService?.StoredDisciple != null;
            JoiningButtonText = "Join";

            IsEditable = group.IsAdmin;
            EditingButtonText = "Edit";

            Name = group.Name;
            Image = group.Image;
            Address = group.Address;
            Latitude = group.Latitude;
            Longitude = group.Longitude;
            MissionStatement = group.MissionStatement;

            SaveCommand = new Command(() => {
                var copy = CurrentGroup?.Clone() as Group ?? new Group();
                copy.Name = Name;
                copy.Image = Image;
                copy.Address = Address;
                copy.Latitude = Latitude;
                copy.Longitude = Longitude;
                copy.MissionStatement = MissionStatement;
                MessagingCenter.Send(CurrentGroup, "upsert");
            });

            JoinCommand = new Command(async () => {
                await dataService.JoinAsync(CurrentGroup);
                MessagingCenter.Send(CurrentGroup, "join");
            });
        }
    }
}

using CHCHALC.Models;
using CHCHALC.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CHCHALC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PublicGroupsPage : ContentPage
    {
        public PublicGroupsPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<Group>(this, "join", async groupParticipated => {
                await Navigation.PopAsync();
            });
        }

        async void CreateGroupAsync(object sender, System.EventArgs e)
        {
            var editPage = new GroupEditPage(new GroupViewModel(new Group()));
            await Navigation.PushAsync(editPage);
        }

        async void GroupTappedAsync(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Group;
            var editPage = new GroupPage(new GroupViewModel(item, true));
            await Navigation.PushAsync(editPage);
        }
    }
}
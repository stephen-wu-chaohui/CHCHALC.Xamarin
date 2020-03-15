using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CHCHALC.Misc;
using CHCHALC.Models;
using CHCHALC.ViewModels;

namespace CHCHALC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MemberListPage : ContentPage
    {
        public MemberListPage(MemberListViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }

        async void JoinOpenGroupsAsync(object sender, System.EventArgs e)
        {
            var editPage = new PublicGroupsPage();
            await Navigation.PushAsync(editPage);
        }

        async void ExistingParticipateTappedAsync(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Group;
            var editPage = new GroupPage(new GroupViewModel(item));
            await Navigation.PushAsync(editPage);
        }
    }
}
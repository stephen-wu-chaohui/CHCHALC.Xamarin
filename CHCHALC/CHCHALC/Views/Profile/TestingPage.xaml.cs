using CHCHALC.Models;
using CHCHALC.ViewModels;

using Xamarin.Forms;

namespace CHCHALC.Views
{
    public partial class TestingPage : ContentPage
    {
        public TestingPage() {
            InitializeComponent();
        }

        void CreateNewClicked(object sender, System.EventArgs e)
        {
            var viewModel = BindingContext as TestingViewModel;
            viewModel.CreateCommand.Execute(null);
            Navigation.PopAsync();
        }

        void LogoutClicked(object sender, System.EventArgs e)
        {
            var viewModel = BindingContext as TestingViewModel;
            viewModel.LogoutCommand.Execute(null);
            Navigation.PopAsync();
        }

        void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var viewModel = BindingContext as TestingViewModel;
            var disciple = e.Item as Disciple;
            viewModel.SetCurrent(disciple);
            Navigation.PopAsync();
        }
    }
}

using System;
using CHCHALC.ViewModels;
using Xamarin.Forms;

namespace CHCHALC.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage() {
            InitializeComponent();
        }

        void ConfirmClicked(object sender, System.EventArgs e)
        {
            var viewModel = BindingContext as ProfileViewModel;
            viewModel.ConfirmCommand.Execute(null);
        }

        async void MoreAccountsClicked(object sender, System.EventArgs e)
        {
            var morePage = new TestingPage();
            await Navigation.PushAsync(morePage);
        }
    }
}

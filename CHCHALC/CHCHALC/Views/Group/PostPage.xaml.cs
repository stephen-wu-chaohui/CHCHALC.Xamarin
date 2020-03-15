using System;
using System.IO;
using CHCHALC.Services;
using CHCHALC.ViewModels;
using Xamarin.Forms;

namespace CHCHALC.Views
{
    public partial class PostPage : ContentPage
    {
        public PostPage(PostViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
            Title = viewModel.Name;
        }

        async void EditButtonClickedAsync(object sender, EventArgs e)
        {
            // BindingContext as EventViewModel
            var editPage = new PostEditPage(BindingContext as PostViewModel);
            await Navigation.PushAsync(editPage);
        }

    }
}

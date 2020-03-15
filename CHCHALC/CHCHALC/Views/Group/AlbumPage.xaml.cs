using System;
using CHCHALC.ViewModels;
using Xamarin.Forms;

namespace CHCHALC.Views
{
    public partial class AlbumPage : ContentPage
    {
        public AlbumPage(AlbumViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
            Title = viewModel.Name;
        }

        async void EditButtonClickedAsync(object sender, EventArgs e)
        {
            var editPage = new AlbumEditPage(BindingContext as AlbumViewModel);
            await Navigation.PushAsync(editPage);
        }

        async void OpenPostList(object sender, EventArgs e)
        {
            var vm = BindingContext as AlbumViewModel;
            var listPage = new PostListPage(new PostListViewModel(vm.CurrentAlbum));
            await Navigation.PushAsync(listPage);
        }
    }
}

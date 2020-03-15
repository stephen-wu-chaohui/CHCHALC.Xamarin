using System;
using CHCHALC.Models;
using CHCHALC.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CHCHALC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlbumListPage : ContentPage
    {
        public AlbumListPage(AlbumListViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }

        async void AlbumTappedAsync(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Album;
            var editPage = new AlbumPage(new AlbumViewModel(item));
            await Navigation.PushAsync(editPage);
        }

        async void AddButtonClickedAsync(object sender, EventArgs e)
        {
            var editPage = new AlbumEditPage(new AlbumViewModel(null));
            await Navigation.PushAsync(editPage);
        }
    }
}
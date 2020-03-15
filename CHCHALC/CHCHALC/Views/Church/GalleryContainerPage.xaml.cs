using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CHCHALC.Models;
using CHCHALC.ViewModels;

namespace CHCHALC.Views
{
    [DesignTimeVisible(false)]
    public partial class GalleryContainerPage : ContentPage
    {
        public GalleryContainerPage()
        {
            InitializeComponent();
        }

        async void OnPlaylistSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is PlayItem item)) {
                return;
            }
            await Navigation.PushAsync(new GalleryItemPage(new GalleryItemViewModel(item)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = BindingContext as GalleryContainerViewModel;

            if (!viewModel.Playlists.Any())
                viewModel.LoadChannelCommand.Execute(null);
        }

        void PlaylistChanged(object sender, System.EventArgs e)
        {
            var viewModel = BindingContext as GalleryContainerViewModel;
            viewModel.CurrentPlayList = playlistPicker.SelectedItem as PlayList;
        }

        void PickerClicked(object sender, System.EventArgs e)
        {
            playlistPicker.Focus();
        }

    }
}

using System;
using System.ComponentModel;
using Xamarin.Forms;
using CHCHALC.ViewModels;

namespace CHCHALC.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class GalleryItemPage : ContentPage
    {
        public GalleryItemPage(GalleryItemViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

            Browser.IsVisible = false;
            Browser.Source = new Uri("https://www.youtube.com/embed/" + viewModel.Item.VideoId);
            Browser.GoForward();

        }

        private void OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            Thumbnail.IsVisible = false;
            Browser.IsVisible = true;

        }
    }
}
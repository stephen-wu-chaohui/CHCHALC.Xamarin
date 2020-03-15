using System;
using System.IO;
using CHCHALC.Services;
using CHCHALC.ViewModels;
using Xamarin.Forms;

namespace CHCHALC.Views
{
    public partial class AlbumEditPage : ContentPage
    {
        public AlbumEditPage(AlbumViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
        }

        async void SaveButtonClickedAsync(object sender, EventArgs e)
        {
            var viewModel = BindingContext as AlbumViewModel;
            viewModel.SaveCommand.Execute(null);
            await Navigation.PopAsync();
        }

        private async void OnImageNameTappedAsync(object sender, EventArgs e)
        {
            var viewModel = BindingContext as AlbumViewModel;

            using (Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync()) {
                if (stream == null)
                    return;

                var copy = new MemoryStream();
                stream.CopyTo(copy);
                stream.Position = 0;
                titleImage.Source = ImageSource.FromStream(() => stream);
                try {
                    var cloudStorage = DependencyService.Get<ICloudStorage>();
                    var fileId = $"{Guid.NewGuid().ToString()}.png";
                    await cloudStorage.UploadFileAsync(fileId, copy.ToArray());
                    copy.Dispose();
                    viewModel.Image = $"https://alife.blob.core.windows.net/pictures/{fileId}";
                } catch (Exception ex) {
                    await DisplayAlert("Failure occurs", ex.Message, "Cancel");
                }
            }
        }
    }
}

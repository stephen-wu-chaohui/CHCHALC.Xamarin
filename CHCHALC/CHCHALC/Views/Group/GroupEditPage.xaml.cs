using System;
using System.IO;
using CHCHALC.Services;
using CHCHALC.ViewModels;
using Xamarin.Forms;

namespace CHCHALC.Views
{
    public partial class GroupEditPage : ContentPage
    {
        public GroupEditPage(GroupViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
            Title = viewModel.Name;
            titleImage.Source = viewModel.Image;
        }

        async void SaveButtonClickedAsync(object sender, EventArgs e)
        {
            var viewModel = BindingContext as GroupViewModel;
            viewModel.SaveCommand.Execute(null);
            await Navigation.PopAsync();
        }

        private async void OnImageNameTappedAsync(object sender, EventArgs e)
        {
            var viewModel = BindingContext as GroupViewModel;

            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream == null)
                return;

            var copy = new MemoryStream();
            stream.CopyTo(copy);
            stream.Position = 0;
            titleImage.Source = ImageSource.FromStream(() => stream);

            var guid = Guid.NewGuid().ToString();

            try {
                var cloudStorage = DependencyService.Get<ICloudStorage>();
                var fileId = $"{guid}.png";
                await cloudStorage.UploadFileAsync(fileId, copy.ToArray());
                viewModel.Image = $"https://alife.blob.core.windows.net/pictures/{fileId}";
            } catch (Exception ex) {
                await DisplayAlert("Failure occurs", ex.Message, "Cancel");
            }
        }

    }
}

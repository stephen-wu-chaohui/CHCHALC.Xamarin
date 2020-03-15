using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CHCHALC.Misc;
using CHCHALC.Models;
using CHCHALC.ViewModels;
using System;
using CHCHALC.Services;
using System.IO;

namespace CHCHALC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostListPage : ContentPage
    {
        public PostListPage(PostListViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }

        async void EventTappedAsync(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Post;
            var editPage = new PostPage(new PostViewModel(item));
            await Navigation.PushAsync(editPage);
        }

        async void AddButtonClickedAsync(object sender, EventArgs e)
        {
            var editPage = new PostEditPage(new PostViewModel(null));
            // var editPage = new EventEditPage();
            await Navigation.PushAsync(editPage);
        }
    }
}
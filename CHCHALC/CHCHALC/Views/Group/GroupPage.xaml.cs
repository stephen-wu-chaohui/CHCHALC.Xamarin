using System;
using System.IO;
using CHCHALC.Services;
using CHCHALC.ViewModels;
using Xamarin.Forms;

namespace CHCHALC.Views
{
    public partial class GroupPage : ContentPage
    {
        public GroupPage(GroupViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
            Title = viewModel.Name;

            if (viewModel.IsEditable) {
                this.ToolbarItems.Add(new ToolbarItem("Edit", "", async () => {
                    var editPage = new GroupEditPage(viewModel);
                    await Navigation.PushAsync(editPage);
                }));
            }

            if (viewModel.IsJoinable) {
                this.ToolbarItems.Add(new ToolbarItem("Join", "", async () => {
                    viewModel.JoinCommand.Execute(null);
                    await Navigation.PopAsync();
                }));
            }
        }

        async void OpenAlbumList(object sender, System.EventArgs e)
        {
            var viewModel = BindingContext as GroupViewModel;
            var listPage = new AlbumListPage(new AlbumListViewModel(viewModel.CurrentGroup));
            await Navigation.PushAsync(listPage);
        }

        async void OpenMemberList(object sender, System.EventArgs e)
        {
            var viewModel = BindingContext as GroupViewModel;
            var listPage = new MemberListPage(new MemberListViewModel(viewModel.CurrentGroup));
            await Navigation.PushAsync(listPage);
        }


    }
}

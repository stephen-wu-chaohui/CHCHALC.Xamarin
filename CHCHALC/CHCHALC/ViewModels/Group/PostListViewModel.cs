using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CHCHALC.Models;
using CHCHALC.Services;
using Xamarin.Forms;
using CHCHALC.Misc;
using System.Collections.Generic;

namespace CHCHALC.ViewModels
{
    public class PostListViewModel : BaseViewModel
    {
        private readonly IContextService ProfileService = DependencyService.Get<IContextService>();
        readonly IDataService dataService = DependencyService.Get<IDataService>();
        public ObservableCollection<Post> Posts { get; set; } = new ObservableCollection<Post>();

        public Album CurrentAlbum { get; }
        private bool AllowToCreate => (ProfileService.StoredDisciple != null);

        public bool HasPosts => Posts.Count > 0;
        public bool NoPosts => Posts.Count == 0;

        public Command LoadCommand { get; set; }

        public PostListViewModel(Album album)
        {
            Title = ProfileService.StoredDisciple?.PreferName ?? "Welcome";

            CurrentAlbum = album;
            LoadCommand = new Command(() => ExecuteLoadCommand());

            MessagingCenter.Subscribe<Post>(this, "upsert", async post => {
                post.AlbumId = album.Id;
                post = await dataService.UpsertAsync(post);
                if (album.Posts == null) {
                    album.Posts = new List<Post>();
                }
                album.Posts.Upsert(post);
                Posts.Upsert(post);
                OnPropertyChanged("HasPosts");
                OnPropertyChanged("NoPosts");
            });

            MessagingCenter.Subscribe<Post>(this, "delete", post => {
                Posts.Delete(post);
                album.Posts.Remove(post);
                OnPropertyChanged("HasPosts");
                OnPropertyChanged("NoPosts");
            });
        }

        private void ExecuteLoadCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (CurrentAlbum == null) {
                IsBusy = false;
                return;
            }

            try {
                Posts.Clear();
                CurrentAlbum.Posts.ForEach(p => Posts.Add(p));
                OnPropertyChanged("HasPosts");
                OnPropertyChanged("NoPosts");
            } catch (Exception ex) {
                Debug.WriteLine(ex);
            } finally {
                IsBusy = false;
            }
        }
    }
}

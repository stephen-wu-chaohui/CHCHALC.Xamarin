using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CHCHALC.Misc;
using CHCHALC.Models;
using CHCHALC.Services;
using Xamarin.Forms;

namespace CHCHALC.ViewModels
{
    public class AlbumListViewModel : BaseViewModel
    {
        private readonly IContextService ProfileService = DependencyService.Get<IContextService>();
        readonly IDataService dataService = DependencyService.Get<IDataService>();
        public ObservableCollection<Album> Albums { get; set; } = new ObservableCollection<Album>();

        public Group CurrentGroup { get; }

        public bool HasAlbums => Albums.Count > 0;
        public bool NoAlbums => Albums.Count == 0;

        public Command LoadCommand { get; set; }

        public AlbumListViewModel(Group group)
        {
            Title = ProfileService.StoredDisciple?.PreferName ?? "Welcome";

            CurrentGroup = group;
            LoadCommand = new Command(() => ExecuteLoadCommand());

            MessagingCenter.Subscribe<Album>(this, "upsert", async album => {
                album.GroupId = group.Id;
                album = await dataService.UpsertAsync(album);
                if (group.Albums == null) {
                    group.Albums = new List<Album>();
                }
                group.Albums.Upsert(album);
                Albums.Upsert(album);
                OnPropertyChanged("HasAlbums");
                OnPropertyChanged("NoAlbums");
            });

            MessagingCenter.Subscribe<Album>(this, "delete", async album => {
                await dataService.Delete(album);
                group.Albums.Remove(album);
                Albums.Delete(album);
                OnPropertyChanged("HasAlbums");
                OnPropertyChanged("NoAlbums");
            });
        }

        void ExecuteLoadCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (CurrentGroup == null) {
                IsBusy = false;
                return;
            }

            try {
                Albums.Clear();
                CurrentGroup.Albums.ForEach(p => Albums.Add(p));
                OnPropertyChanged("HasAlbums");
                OnPropertyChanged("NoAlbums");
            } catch (Exception ex) {
                Debug.WriteLine(ex);
            } finally {
                IsBusy = false;
            }
        }
    }
}

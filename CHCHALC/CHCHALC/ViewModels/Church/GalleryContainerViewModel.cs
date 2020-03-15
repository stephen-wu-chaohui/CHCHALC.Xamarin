using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CHCHALC.Models;
using CHCHALC.Services;
using Xamarin.Forms;

namespace CHCHALC.ViewModels
{
    public class GalleryContainerViewModel : BaseViewModel
    {
        public ObservableCollection<PlayList> Playlists { get; set; }
        public ObservableCollection<PlayItem> PlayItems { get; set; }

        public Command LoadChannelCommand { get; set; }
        public Command LoadPlaylistCommand { get; set; }

        private PlayList _currentPlayList;
        public PlayList CurrentPlayList {
            get { return _currentPlayList; }
            set {
                _currentPlayList = value;
                Title = _currentPlayList?.Text ?? "Videos";
                if (LoadPlaylistCommand != null) {
                    LoadPlaylistCommand.Execute(null);
                }
            }
        }

        public IVideoRepository VideoRepository => DependencyService.Get<IVideoRepository>();

        public GalleryContainerViewModel()
        {
            Title = "Gallery";
            Playlists = new ObservableCollection<PlayList>();
            LoadChannelCommand = new Command(async () => await ExecuteLoadChannelCommand());

            CurrentPlayList = null;
            PlayItems = new ObservableCollection<PlayItem>();
            LoadPlaylistCommand = new Command(async () => await ExecuteLoadPlaylistCommand());
        }

        async Task ExecuteLoadChannelCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try {
                Playlists.Clear();
                var items = await VideoRepository.LoadChannelAsync("");

                foreach (var item in items)
                {
                    Playlists.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            CurrentPlayList = Playlists.FirstOrDefault(pl => pl.Id == "PLlZwsUoL6TOaEwk2wkvVrrZnsSiCl7VKb");
        }

        async Task ExecuteLoadPlaylistCommand()
        {
            if (IsBusy || string.IsNullOrEmpty(CurrentPlayList?.Id))
                return;

            IsBusy = true;

            try {
                PlayItems.Clear();
                var items = await VideoRepository.LoadPlayItemsAsync(CurrentPlayList.Id);
                foreach (var item in items) {
                    PlayItems.Add(item);
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex);
            } finally {
                IsBusy = false;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public class PlaylistViewModel
        : IPlaylistViewModel
    {
        public PlaylistViewModel(IPlaylist playlist)
        {
            if (playlist == null)
                throw new ArgumentNullException("playlist");

            this.playlist = playlist;
        }

        private readonly IPlaylist playlist;
        private readonly ObservableCollection<IPlaylistItemViewModel> items = new ObservableCollection<IPlaylistItemViewModel>();

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Uri Id
        {
            get { return playlist.Location; }
        }

        public string CreatorName
        {
            get { return playlist.CreatorName; }
        }

        public string Name
        {
            get { return playlist.CatalogName; }
        }

        public string Number
        {
            get { return playlist.Number > 0 ? playlist.Number.ToString() : string.Empty; }
        }

        public string Year
        {
            get { return playlist.FromDate.Year.ToString(); }
        }

        public object Image
        {
            get { return playlist.ThumbnailData != null && playlist.ThumbnailData.Length > 0 ?
                (object)playlist.ThumbnailData
                : playlist.Thumbnail;
            }
        }

        public IEnumerable<IPlaylistItemViewModel> Items
        {
            get { return items; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddItem(IPlaylistItemViewModel item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            items.Add(item);
        }
    }
}

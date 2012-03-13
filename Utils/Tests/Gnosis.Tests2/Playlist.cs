using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class Playlist
        : Entity
    {
        private readonly ObservableCollection<PlaylistItem> playlistItems = new ObservableCollection<PlaylistItem>();

        public IEnumerable<PlaylistItem> PlaylistItems { get { return playlistItems; } }
    }
}

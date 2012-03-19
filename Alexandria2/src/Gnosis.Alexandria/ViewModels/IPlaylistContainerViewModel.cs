using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IPlaylistContainerViewModel
    {
        IEnumerable<IPlaylistViewModel> Playlists { get; }

        void AddPlaylist(IPlaylistViewModel playlist);
        void RemovePlaylist(IPlaylistViewModel playlist);
    }
}

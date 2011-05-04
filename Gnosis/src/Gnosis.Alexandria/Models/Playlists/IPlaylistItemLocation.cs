using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Playlists
{
    public interface IPlaylistItemLocation : IValue
    {
        IPlaylist Playlist { get; }
        Uri Location { get; }
    }
}

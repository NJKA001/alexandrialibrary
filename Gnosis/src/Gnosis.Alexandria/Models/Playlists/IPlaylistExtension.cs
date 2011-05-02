using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Playlists
{
    public interface IPlaylistExtension : IModel
    {
        IPlaylist Playlist { get; }
        Uri Application { get; }
        string Content { get; }
    }
}

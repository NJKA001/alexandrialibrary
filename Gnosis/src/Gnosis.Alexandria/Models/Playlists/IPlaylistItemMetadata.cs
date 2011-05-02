using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Playlists
{
    public interface IPlaylistItemMetadata : IModel
    {
        IPlaylist Playlist { get; }
        Uri Relationship { get; }
        string Content { get; }
    }
}

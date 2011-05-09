using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Playlists
{
    public interface IPlaylistExtension : IValue
    {
        IPlaylist Playlist { get; }
        Uri Application { get; }
        string Content { get; }
    }
}

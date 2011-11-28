using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Gnosis.Data;

namespace Gnosis.Alexandria.Models.Playlists
{
    public interface IPlaylistItem : IEntity
    {
        string Title { get; set; }
        string Creator { get; set; }
        string Comment { get; set; }
        Uri Info { get; set; }
        Uri ImagePath { get; set; }
        string Album { get; set; }
        uint TrackNumber { get; set; }
        TimeSpan Duration { get; set; }

        IEnumerable<IPlaylistExtension> Extensions { get; }
        IEnumerable<IPlaylistLink> Links { get; }
        IEnumerable<IPlaylistItemLocation> Locations { get; }
        IEnumerable<IPlaylistMetadata> Metadata { get; }
    }
}

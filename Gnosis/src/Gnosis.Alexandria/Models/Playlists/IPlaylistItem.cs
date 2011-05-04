using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Playlists
{
    public interface IPlaylistItem : IEntity
    {
        IPlaylist Playlist { get; }
        string Title { get; set; }
        string Creator { get; set; }
        string Comment { get; set; }
        Uri Info { get; set; }
        Uri ImagePath { get; set; }
        string Album { get; set; }
        uint TrackNumber { get; set; }
        TimeSpan Duration { get; set; }

        IEnumerable<IPlaylistItemExtension> Extensions { get; }
        IEnumerable<IPlaylistItemLink> Links { get; }
        IEnumerable<IPlaylistItemLocation> Locations { get; }
        IEnumerable<IPlaylistItemMetadata> Metadata { get; }

        void AddExtension(IPlaylistItemExtension extension);
        void RemoveExtension(IPlaylistItemExtension extension);

        void AddLink(IPlaylistItemLink link);
        void RemoveLink(IPlaylistItemLink link);

        void AddLocation(IPlaylistItemLocation location);
        void RemoveLocation(IPlaylistItemLocation location);

        void AddMetadata(IPlaylistItemMetadata metadata);
        void RemoveMetadata(IPlaylistItemMetadata metadata);
    }
}

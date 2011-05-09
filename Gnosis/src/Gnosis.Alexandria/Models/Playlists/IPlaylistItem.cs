using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;
using Gnosis.Core.Collections;

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

        IOrderedSet<IPlaylistExtension> Extensions { get; }
        IOrderedSet<IPlaylistLink> Links { get; }
        IOrderedSet<IPlaylistItemLocation> Locations { get; }
        IOrderedSet<IPlaylistMetadata> Metadata { get; }
    }
}

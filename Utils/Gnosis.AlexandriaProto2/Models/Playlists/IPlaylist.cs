﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Gnosis.Data;

namespace Gnosis.Alexandria.Models.Playlists
{
    public interface IPlaylist : IEntity
    {
        Uri Location { get; }
        string MediaType { get; }
        string Title { get; set; }
        string Creator { get; set; }
        string Comment { get; set; }
        Uri OriginalLocation { get; set; }
        Uri Info { get; set; }
        string PlaylistIdentifier { get; set; }
        Uri ImagePath { get; set; }
        DateTime CreatedDate { get; set; }
        string Copyright { get; set; }

        IEnumerable<IPlaylistAttribution> Attributions { get; }
        IEnumerable<IPlaylistExtension> Extensions { get; }
        IEnumerable<IPlaylistItem> Items { get; }
        IEnumerable<IPlaylistLink> Links { get; }
        IEnumerable<IPlaylistMetadata> Metadata { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;
using Gnosis.Core.Collections;

namespace Gnosis.Alexandria.Models.Playlists
{
    [Table("Playlist")]
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

        [OneToMany("PlaylistAttributions", HasSequence = true)]
        IOrderedSet<IPlaylistAttribution> Attributions { get; }

        [OneToMany("PlaylistExtensions", HasSequence = true)]
        IOrderedSet<IPlaylistExtension> Extensions { get; }
        
        [OneToMany("PlaylistItems", HasSequence = true)]
        IOrderedSet<IPlaylistItem> Items { get; }

        [OneToMany("PlaylistLinks", HasSequence = true)]
        IOrderedSet<IPlaylistLink> Links { get; }

        [OneToMany("PlaylistMetadata", HasSequence = true)]
        IOrderedSet<IPlaylistMetadata> Metadata { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public interface IRepository
    {
        IEnumerable<Artist> Artists { get; }
        IEnumerable<Album> Albums { get; }
        IEnumerable<Track> Tracks { get; }
        IEnumerable<Playlist> Playlists { get; }
        IEnumerable<PlaylistItem> PlaylistItems { get; }
        IEnumerable<Feed> Feeds { get; }
        IEnumerable<FeedItem> FeedItems { get; }

        void Initialize();
        void Save(IEnumerable<Entity> entities);
        void Delete(IEnumerable<Entity> entities);
    }
}

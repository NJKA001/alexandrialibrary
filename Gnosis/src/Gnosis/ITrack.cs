using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ITrack
    {
        Guid Id { get; }
        string Title { get; }
        uint Number { get; }
        TimeSpan Duration { get; }

        Guid Artist { get; }
        string ArtistName { get; }
        Guid Album { get; }
        string AlbumTitle { get; }
        
        Uri AudioLocation { get; }
        IMediaType AudioType { get; }
        Uri Thumbnail { get; }
    }
}

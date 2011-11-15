using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ITrack
        : IApplication
    {
        string Title { get; }
        uint Number { get; }
        TimeSpan Duration { get; }

        Uri Artist { get; }
        string ArtistName { get; }
        Uri Album { get; }
        string AlbumTitle { get; }
        
        Uri AudioLocation { get; }
        IMediaType AudioType { get; }
        Uri Thumbnail { get; }
    }
}

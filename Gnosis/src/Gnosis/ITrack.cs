using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ITrack
        : IMedia
    {
        string Title { get; }
        string ArtistName { get; }
        string AlbumTitle { get; }
        int Number { get; }
        TimeSpan Duration { get; }
        Uri Artist { get; }
        Uri Album { get; }
        IImage Thumbnail { get; }
    }
}

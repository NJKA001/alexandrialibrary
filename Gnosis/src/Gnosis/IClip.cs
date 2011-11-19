using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IClip
        : IApplication
    {
        string Title { get; }
        uint Number { get; }
        TimeSpan Duration { get; }
        int Height { get; }
        int Width { get; }

        Uri Creator { get; }
        string CreatorName { get; }
        Uri Album { get; }
        string AlbumTitle { get; }

        Uri Target { get; }
        IMediaType TargetType { get; }
        Uri Thumbnail { get; }
    }
}

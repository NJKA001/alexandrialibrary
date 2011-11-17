using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IPic
        : IApplication
    {
        string Title { get; }
        uint Number { get; }
        int Height { get; }
        int Width { get; }

        Uri Creator { get; }
        string CreatorName { get; }
        Uri Album { get; }
        string AlbumTitle { get; }

        Uri ImageLocation { get; }
        IMediaType ImageType { get; }
        Uri Thumbnail { get; }
    }
}

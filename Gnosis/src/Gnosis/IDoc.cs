using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IDoc
        : IApplication
    {
        string Title { get; }
        uint Number { get; }
        uint Length { get; }

        Uri Creator { get; }
        string CreatorName { get; }
        Uri Album { get; }
        string AlbumTitle { get; }

        Uri TextLocation { get; }
        IMediaType TextType { get; }
        Uri Thumbnail { get; }
    }
}

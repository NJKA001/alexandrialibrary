using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMediaDetail
    {
        ITag Tag { get; }
        IImage ArtistThumbnail { get; }
        IImage CollectionThumbnail { get; }
    }
}

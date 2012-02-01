using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMediaFactory
    {
        IMedia Create(Uri location, IContentType type);
        IEnumerable<string> GetMediaTypes();

        void MapMediaType(string mediaType, Func<Uri, IContentType, IMedia> createFunction);
    }
}

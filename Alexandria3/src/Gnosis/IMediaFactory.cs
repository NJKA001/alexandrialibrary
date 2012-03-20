using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMediaFactory
    {
        IMediaType DefaultType { get; }

        IMedia GetMedia(Uri location);
        IMediaType GetMediaType(string name);

        void MapFileExtensions(string mediaType, IEnumerable<string> fileExtensions);
        void MapLegacyMediaTypes(string mediaType, IEnumerable<string> legacyMediaTypes);
        void MapMagicNumbers(string mediaType, byte[] magicNumbers);
    }
}

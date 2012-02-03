using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMediaFactory
    {
        IMediaType DefaultType { get; }

        IMediaType GetTypeByCode(string code);
        IMediaType GetTypeByLocation(Uri location);
        
        IMedia Create(Uri location);

        void MapCreateFunction(string mediaType, Func<Uri, IMediaType, IMedia> createFunction);
        void MapFileExtensions(string mediaType, IEnumerable<string> fileExtensions);
        void MapMagicNumbers(string mediaType, byte[] magicNumbers);
    }
}

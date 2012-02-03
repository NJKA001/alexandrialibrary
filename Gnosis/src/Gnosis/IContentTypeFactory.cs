using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IContentTypeFactory
    {
        IContentType Default { get; }

        IContentType GetByCode(string code);
        IContentType GetByLocation(Uri location);
        
        IMedia Create(Uri location);

        void MapFileExtensions(string name, IEnumerable<string> fileExtensions);
        void MapMagicNumbers(string name, byte[] magicNumbers);

        void MapMediaType(string mediaType, Func<Uri, IContentType, IMedia> createFunction);
    }
}

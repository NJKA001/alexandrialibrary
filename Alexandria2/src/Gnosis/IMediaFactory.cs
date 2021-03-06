﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMediaFactory
    {
        IMedia Default { get; }
        
        IMedia Create(Uri location);
        IMediaType GetMediaType(string name);

        void MapCreateFunction(string mediaType, Func<Uri, IMediaType, IMedia> createFunction);
        void MapFileExtensions(string mediaType, IEnumerable<string> fileExtensions);
        void MapLegacyMediaTypes(string mediaType, IEnumerable<string> legacyMediaTypes);
        void MapMagicNumbers(string mediaType, byte[] magicNumbers);
    }
}

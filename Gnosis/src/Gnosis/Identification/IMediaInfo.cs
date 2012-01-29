using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Identification
{
    public interface IMediaInfo
    {
        Uri Location { get; }

        IContentType ResponseContentType { get; }

        IMediaType LocationMediaType { get; }
        IMediaType MagicNumberMediaType { get; }
        IMediaType ContentMediaType { get; }

        ICharacterSet BomCharacterSet { get; }
        ICharacterSet ContentCharacterSet { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Identification
{
    public interface IMediaInfo
    {
        Uri Location { get; }
        string FileExtension { get; }
        
        IContentType ResponseContentType { get; }
        
        ICharacterSet BomCharacterSet { get; }

        byte[] ContentMagicNumber { get; }
        IMediaType ContentMediaType { get; }
        ICharacterSet ContentCharacterSet { get; }

        IContentType ToContentType();
    }
}

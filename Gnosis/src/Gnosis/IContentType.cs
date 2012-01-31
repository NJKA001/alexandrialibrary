using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IContentType
    {
        IMediaType MediaType { get; }
        ICharacterSet CharSet { get; }
        string Boundary { get; }

        IMedia CreateMedia(Uri location);
    }
}

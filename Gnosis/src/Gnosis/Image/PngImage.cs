using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Image
{
    public class PngImage
        : ImageBase
    {
        public PngImage(Uri location, IMediaType mediaType)
            : base(location, mediaType)
        {
        }
    }
}

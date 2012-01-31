using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Image
{
    public class PngImage
        : ImageBase
    {
        public PngImage(Uri location, IContentType mediaType)
            : base(location, mediaType)
        {
        }
    }
}

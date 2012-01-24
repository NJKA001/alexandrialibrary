using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Image
{
    public class BitmapImage
        : ImageBase
    {
        public BitmapImage(Uri location, IMediaType mediaType)
            : base(location, mediaType)
        {
        }
    }
}

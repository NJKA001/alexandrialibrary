using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Image
{
    public class GifImage
        : ImageBase
    {
        public GifImage(Uri location)
            : base(location, MediaType.ImageGif)
        {
        }
    }
}

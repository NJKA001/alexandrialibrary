using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Image
{
    public class JpegImage
        : ImageBase
    {
        public JpegImage(Uri location)
            : base(location, MediaType.ImageJpeg)
        {
        }
    }
}

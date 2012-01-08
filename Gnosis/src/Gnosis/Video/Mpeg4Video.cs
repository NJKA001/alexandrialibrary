using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Video
{
    public class Mpeg4Video
        : VideoBase
    {
        public Mpeg4Video(Uri location)
            : base(location, MediaType.VideoMpeg4)
        {
        }
    }
}

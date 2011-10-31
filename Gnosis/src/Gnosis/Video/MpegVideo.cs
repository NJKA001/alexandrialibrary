using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Video
{
    public class MpegVideo
        : VideoBase
    {
        public MpegVideo(Uri location)
            : base(location, MediaType.VideoMpeg)
        {
        }
    }
}

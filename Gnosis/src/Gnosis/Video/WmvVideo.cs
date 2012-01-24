using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Video
{
    public class WmvVideo
        : VideoBase
    {
        public WmvVideo(Uri location, IMediaType mediaType)
            : base(location, mediaType)
        {
        }
    }
}

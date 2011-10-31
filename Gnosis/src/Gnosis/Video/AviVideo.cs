using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Video
{
    public class AviVideo
        : VideoBase
    {
        public AviVideo(Uri location)
            : base(location, MediaType.VideoAvi)
        {
        }
    }
}

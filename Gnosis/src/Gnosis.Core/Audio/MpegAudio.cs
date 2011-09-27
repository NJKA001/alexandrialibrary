using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Audio
{
    public class MpegAudio
        : AudioBase, IMedia
    {
        public MpegAudio(Uri location)
            : base(location, MediaType.AudioMpeg)
        {
        }
    }
}

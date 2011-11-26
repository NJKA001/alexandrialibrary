using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Audio
{
    public interface IMpegAudio
        : IAudio
    {
        void SetTag(ITag tag);
        void RemoveTag(ITag tag);
        void Save();
    }
}

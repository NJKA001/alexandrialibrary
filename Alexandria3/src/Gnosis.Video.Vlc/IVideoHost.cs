using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Video.Vlc
{
    public interface IVideoHost
    {
        bool IsOpen { get; }

        void Open(IVideoPlayer videoPlayer);
    }
}

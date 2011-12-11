using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IVideoPlayer
    {
        void Load(Uri location);
    }
}

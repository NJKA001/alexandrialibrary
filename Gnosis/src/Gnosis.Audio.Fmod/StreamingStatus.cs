using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Audio.Fmod
{
    public enum StreamingStatus
    {
        None = 0,
        Initializing,
        Healthy,
        Starving
    }
}

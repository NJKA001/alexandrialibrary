using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public enum MediaSupertype : byte
    {
        application = 0,
        audio = 1,
        image = 2,
        message = 3,
        model = 4,
        multipart = 5,
        text = 6,
        video = 7
    }
}

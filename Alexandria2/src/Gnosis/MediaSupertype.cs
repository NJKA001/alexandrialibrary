using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public enum MediaSupertype : byte
    {
        Application = 0,
        Audio = 1,
        Image = 2,
        Message = 3,
        Model = 4,
        Multipart = 5,
        Text = 6,
        Video = 7
    }
}

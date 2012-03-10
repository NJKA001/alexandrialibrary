using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public enum TargetType : byte
    {
        None = 0,
        Application = 1,
        Audio = 2,
        Image = 3,
        Message = 4,
        Model = 5,
        Multipart = 6,
        Text = 7,
        Video = 8
    }
}

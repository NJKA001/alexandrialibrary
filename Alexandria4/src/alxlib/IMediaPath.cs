using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IMediaPath
    {
        bool IsLocal { get; }

        string AbsolutePath { get; }
    }
}

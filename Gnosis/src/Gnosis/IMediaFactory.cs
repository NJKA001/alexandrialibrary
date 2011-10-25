using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMediaFactory
    {
        IMedia Create(Uri location);
        IMedia Create(Uri location, IMediaType type);
    }
}

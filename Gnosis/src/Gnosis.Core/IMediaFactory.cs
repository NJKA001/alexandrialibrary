using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IMediaFactory
    {
        IMedia Create(Uri location);
    }
}

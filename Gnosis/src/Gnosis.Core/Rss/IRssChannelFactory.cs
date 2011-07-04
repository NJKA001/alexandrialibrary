using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Rss
{
    public interface IRssChannelFactory
    {
        IRssChannel Create(Uri location);
    }
}

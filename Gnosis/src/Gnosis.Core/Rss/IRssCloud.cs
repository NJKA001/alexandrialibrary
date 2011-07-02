using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Rss
{
    public interface IRssCloud
    {
        string Domain { get; }
        int Port { get; }
        string Path { get; }
        string RegisterProcedure { get; }
        RssCloudProtocol Protocol { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Rss
{
    public interface IRssCloud
        : IRssElement
    {
        string Domain { get; }
        int Port { get; }
        string Path { get; }
        string RegisterProcedure { get; }
        RssCloudProtocol Protocol { get; }
    }
}

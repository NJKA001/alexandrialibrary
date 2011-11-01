using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.Google.Data
{
    public interface IGoogleDataFeedLink
        : IGoogleDataElement
    {
        string Rel { get; }
        Uri Href { get; }
        int CountHint { get; }
    }
}

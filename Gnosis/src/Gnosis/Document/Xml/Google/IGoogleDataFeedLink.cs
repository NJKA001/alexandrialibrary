using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Google
{
    public interface IGoogleDataFeedLink
        : IGoogleDataElement
    {
        string Rel { get; }
        Uri Href { get; }
        int CountHint { get; }
    }
}

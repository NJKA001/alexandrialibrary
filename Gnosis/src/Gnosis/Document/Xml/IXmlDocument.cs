using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml
{
    public interface IXmlDocument
        : IDocument
    {
        IXmlElement Xml { get; }
    }
}

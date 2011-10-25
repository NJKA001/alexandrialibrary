using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Xspf
{
    public interface IXspfLink
        : IXspfElement
    {
        Uri Rel { get; }
        Uri Content { get; }
    }
}

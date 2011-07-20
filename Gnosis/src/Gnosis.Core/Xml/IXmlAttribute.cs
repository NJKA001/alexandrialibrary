using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public interface IXmlAttribute
    {
        string Prefix { get; }
        string Name { get; }
        string Value { get; }
    }
}

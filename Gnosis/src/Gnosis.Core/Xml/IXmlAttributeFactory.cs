using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public interface IXmlAttributeFactory
    {
        string AttributeName { get; }
        bool IsValidFor(IXmlAttribute attribute);
        IXmlAttribute Create(System.Xml.XmlAttribute attribute);
    }
}

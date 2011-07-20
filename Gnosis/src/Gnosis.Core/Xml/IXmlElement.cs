using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public interface IXmlElement
    {
        string Prefix { get; }
        string Name { get; }
        IXmlElement Parent { get; }
        IEnumerable<IXmlComment> Comments { get; }
        IEnumerable<IXmlAttribute> Attributes { get; }
        IEnumerable<IXmlElement> Children { get; }
        IXmlCharacterData CharacterData { get; }

        IXmlAttribute GetAttribute(string name);
        IXmlAttribute GetAttribute(string name, string prefix);
        IEnumerable<IXmlNamespace> GetNamespaces();
    }
}

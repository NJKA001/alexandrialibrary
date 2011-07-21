using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public interface IXmlElement
    {
        IXmlQualifiedName Name { get; }
        IXmlElement Parent { get; }
        IEnumerable<IXmlComment> Comments { get; }
        IEnumerable<IXmlAttribute> Attributes { get; }
        IEnumerable<IXmlElement> Children { get; }
        IXmlCharacterData CharacterData { get; }

        IXmlAttribute GetAttribute(string name);
        IEnumerable<IXmlNamespace> GetNamespaces();

        void AddChild(IXmlElement child);
    }
}

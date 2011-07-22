using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public interface IXmlElement
        : IXmlMarkup
    {
        IXmlQualifiedName Name { get; }
        IXmlElement ParentElement { get; }
        IEnumerable<IXmlAttribute> Attributes { get; }
        IEnumerable<IXmlComment> Comments { get; }
        IEnumerable<IXmlElement> ChildElements { get; }
        IEnumerable<IXmlNamespace> Namespaces { get; }
        IEnumerable<IXmlCharacterData> CharacterDataSections { get; }
    }
}

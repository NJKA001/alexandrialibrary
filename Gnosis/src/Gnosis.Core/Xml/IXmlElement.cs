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
        IXmlElement Parent { get; }
        IEnumerable<IXmlComment> Comments { get; }
        IEnumerable<IXmlAttribute> Attributes { get; }
        IEnumerable<IXmlElement> Children { get; }
        IXmlCharacterData CharacterData { get; }
        IEnumerable<IXmlNamespace> Namespaces { get; }

        IEnumerable<T> Where<T>(Func<T, bool> predicate) where T : class, IXmlNode;

        void AddChild(IXmlElement element);
        //TODO: Implement this instead to support having a sequence of IMarkup that preserves the order of the original document
        //void SetParent(IXmlElement parent);
    }
}

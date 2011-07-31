using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public interface IElement
        : IMarkup
    {
        IQualifiedName Name { get; }
        IElement ParentElement { get; }
        IEnumerable<IAttribute> Attributes { get; }
        IEnumerable<IComment> Comments { get; }
        IEnumerable<IElement> ChildElements { get; }
        IEnumerable<INamespace> Namespaces { get; }
        IEnumerable<ICharacterData> CharacterDataSections { get; }

        void AddAttribute(IAttribute attribute);
    }
}

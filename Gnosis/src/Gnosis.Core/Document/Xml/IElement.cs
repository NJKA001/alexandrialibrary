using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml
{
    public interface IElement
        : IMarkup
    {
        IQualifiedName Name { get; }
        IElement ParentElement { get; }
        IEnumerable<IAttribute> Attributes { get; }
        IEnumerable<IComment> Comments { get; }
        IEnumerable<IElement> ChildElements { get; }
        IEnumerable<INamespaceDeclaration> Namespaces { get; }
        IEnumerable<ICharacterData> CharacterDataSections { get; }
        INamespaceDeclaration CurrentNamespace { get; }

        void AddAttribute(IAttribute attribute);

        INamespaceDeclaration FindNamespace(string alias);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public interface IDocumentType
        : INode
    {
        string RootElement { get; }
        EntityVisibility Visibility { get; }
        string FormalPublicIdentifier { get; }
        Uri Uri { get; }

        IEnumerable<IEntity> ChildEntities { get; }
    }
}

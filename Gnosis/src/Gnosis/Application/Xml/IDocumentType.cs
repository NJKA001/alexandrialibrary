using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml
{
    public interface IDocumentType
        : INode
    {
        string RootElement { get; }
        EntityVisibility Visibility { get; }
        string FormalPublicIdentifier { get; }
        string SystemIdentifier { get; }

        IEnumerable<IEntity> ChildEntities { get; }
    }
}

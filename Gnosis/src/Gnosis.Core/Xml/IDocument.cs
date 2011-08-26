using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public interface IDocument
        : IMarkup
    {
        IDeclaration Declaration { get; }
        IDocumentType DocumentType { get; }
        IEnumerable<IProcessingInstruction> ProcessingInstructions { get; }
        IEnumerable<IComment> Comments { get; }
        IElement Root { get; }
    }
}

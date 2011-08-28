using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml
{
    public interface IElementFactory
    {
        string ElementName { get; }
        bool IsValidFor(IElement element);
        IElement Create(INode parent, IQualifiedName name);
    }
}

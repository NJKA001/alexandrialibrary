using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml
{
    public interface IAttribute
        : IMarkup
    {
        IElement ParentElement { get; }
        IQualifiedName Name { get; }
        string Value { get; }
    }
}

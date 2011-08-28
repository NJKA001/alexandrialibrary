using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml
{
    public interface IAttribute
        : IMarkup
    {
        IElement ParentElement { get; }
        IQualifiedName Name { get; }
        string Value { get; }
    }
}

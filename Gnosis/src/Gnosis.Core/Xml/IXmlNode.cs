using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public interface IXmlNode
    {
        IXmlNode Parent { get; set; }
        IEnumerable<IXmlNode> Children { get; }

        IEnumerable<T> Where<T>(Func<T, bool> predicate) where T : class, IXmlNode;
    }
}

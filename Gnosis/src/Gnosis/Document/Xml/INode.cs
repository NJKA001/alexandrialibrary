using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml
{
    public interface INode
    {
        INode Parent { get; }
        IEnumerable<INode> Children { get; }

        IEnumerable<T> Where<T>(Func<T, bool> predicate) where T : class, INode;

        void AddChild(INode child);
    }
}
